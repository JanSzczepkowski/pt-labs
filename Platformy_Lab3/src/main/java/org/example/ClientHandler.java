package org.example;

import java.io.*;
import java.net.Socket;
import java.util.ArrayList;
import java.util.Scanner;

public class ClientHandler implements Runnable{
    private Socket socket;
    private ObjectOutputStream outputStream;
    private ObjectInputStream inputStream;

    public ClientHandler(Socket socket){
        try{
            this.socket = socket;
            this.outputStream = new ObjectOutputStream(socket.getOutputStream());
            this.inputStream = new ObjectInputStream(socket.getInputStream());
        } catch(IOException e){
            closeEverything(socket, outputStream, inputStream);
        }
    }

    @Override
    public void run(){
        String messageFromClient;
        while(!socket.isClosed()) {
            broadcastMessage(0, "SERVER: ready");
            int num = 0;
            try {
                Message message = (Message) inputStream.readObject();
                if (message.getContent().equals("")) {
                    num = message.getNumber();
                }
            } catch (IOException e) {
                throw new RuntimeException(e);
            } catch (ClassNotFoundException e) {
                throw new RuntimeException(e);
            }

            broadcastMessage(0, "SERVER: ready for messages");
            while (num > 0) {
                try {
                    Message message = (Message) inputStream.readObject();
                    System.out.println(message.getNumber() + ". " + message.getContent());
                    num--;
                } catch (IOException e) {
                    throw new RuntimeException(e);
                } catch (ClassNotFoundException e) {
                    throw new RuntimeException(e);
                }
            }
            broadcastMessage(0,"SERVER: finished");

            closeEverything(socket, outputStream, inputStream);
        }
    }

    public void closeEverything(Socket socket, ObjectOutputStream outputStream, ObjectInputStream inputStream){
        try{
            if (inputStream != null){
                inputStream.close();
            }
            if(outputStream != null){
                outputStream.close();
            } if(socket != null){
                socket.close();
            }
        } catch (IOException e){
            e.printStackTrace();
        }
    }

    public void broadcastMessage(int n,String message){
        try {
            outputStream.writeObject(new Message(n, message));

        } catch(IOException e){
            closeEverything(socket, outputStream, inputStream);
        }
    }
}