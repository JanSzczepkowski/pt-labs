package org.example;

import java.io.*;
import java.net.Socket;
import java.util.Scanner;

public class Client {
    private Socket socket;
    private ObjectOutputStream outputStream;
    private ObjectInputStream inputStream;

    public Client(Socket socket){
        try{
            this.socket = socket;
            this.outputStream = new ObjectOutputStream(socket.getOutputStream());
            this.inputStream = new ObjectInputStream(socket.getInputStream());

        } catch(IOException e){
            closeEverything(socket, outputStream, inputStream);
        }
    }

    public synchronized void sendMessage(){
        try {
            Scanner scanner = new Scanner(System.in);
            int n = Integer.parseInt(scanner.nextLine());
            outputStream.writeObject(new Message(n,""));
            for(int i = 0; i < n; i++){


                String messageToSend = scanner.nextLine();
                outputStream.writeObject(new Message(i + 1,messageToSend));

            }
        } catch(IOException e){
            closeEverything(socket, outputStream, inputStream);
        }
        //closeEverything(socket, outputStream, inputStream);
    }

    public void receiveMessage(){
        new Thread(new Runnable() {
            @Override
            public void run() {

                Message messageFromServer;
                while(!socket.isClosed()){
                    try{
                        messageFromServer = (Message) inputStream.readObject();
                        System.out.println(messageFromServer.getContent());
                    } catch (ClassNotFoundException e) {
                        closeEverything(socket, outputStream, inputStream);
                    } catch (IOException e) {
                        closeEverything(socket, outputStream, inputStream);
                    }
                }
            }
        }).start();
    }

    public void closeEverything(Socket socket, ObjectOutputStream outputStream, ObjectInputStream objectInputStream){
        try{
            if (outputStream != null){
                outputStream.close();
            }
            if(objectInputStream != null){
                objectInputStream.close();
            } if(socket != null){
                this.socket.close();
            }
        } catch (IOException e){
            e.printStackTrace();
        }

    }

    public static void main(String[] args) throws IOException {
        Socket socket = new Socket("localhost", 1234);
        Client client = new Client(socket);
        client.receiveMessage();
        client.sendMessage();
    }
}