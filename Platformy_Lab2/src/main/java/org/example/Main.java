package org.example;
import java.util.Scanner;

//TIP To <b>Run</b> code, press <shortcut actionId="Run"/> or
// click the <icon src="AllIcons.Actions.Execute"/> icon in the gutter.
public class Main {
    public static void main(String[] args) {
        int numberOfThreads = Integer.parseInt(args[0]);
        MyQueue queue = new MyQueue();
        Results results = new Results();

        queue.addTask(7); //1
        queue.addTask(13);
        queue.addTask(19);
        queue.addTask(11);
        queue.addTask(12);
        queue.addTask(9);
        queue.addTask(5);
        queue.addTask(211);
        queue.addTask(828);
        queue.addTask(949); //10


        /*MyThread t1 = new MyThread(queue, results); //wrocic
        MyThread t2 = new MyThread(queue, results);
        MyThread t3 = new MyThread(queue, results);*/


        MyThread[] threads = new MyThread[numberOfThreads];
        for (int i = 0; i < numberOfThreads; i++) {
            threads[i] = new MyThread(queue, results, i + 1);
            threads[i].start();
        }

        /*t1.start(); //start odpali run
        t2.start();
        t3.start();*/

        Scanner scanner = new Scanner(System.in);
        while (true) {
            String input = scanner.nextLine();
            if (input.equals("q")) {
                for(int i = 0; i < numberOfThreads; i++){
                    threads[i].interrupt();
                }
                return;
            } else if(input.equals("p")){
                results.printResults();
            } else {
                queue.addTask(Integer.parseInt(input));
            }
        }
    }
}