package org.example;

import java.util.Queue;

public class MyThread extends Thread{
    private MyQueue queue;
    private Results results;

    private int number;

    public MyThread(MyQueue queue, Results results, int num) {
        this.queue = queue;
        this.results = results;
        this.number = num;
    }

    @Override
    public void run(){
        while(true){
            Task task = null;
            try {
                task = queue.getTask();
            } catch (InterruptedException e) {
                throw new RuntimeException(e);
            }
            if(task != null){
                try{
                    Thread.sleep(1000); //symulacja dlugich obliczen
                } catch(InterruptedException e){
                    return;
                }
                int num = task.getNumber();
                boolean isPrime = checkIfPrime(num);
                results.addResult(num, isPrime);
            }
        }
    }

    private boolean checkIfPrime(int n){
        if(n < 1) return false;
        for(int i = 2; i * i <= n; i++){
            if (n % i == 0) return false;
        }
        return true;
    }
}
