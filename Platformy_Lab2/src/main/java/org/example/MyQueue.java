package org.example;
import java.util.LinkedList;
import java.util.Queue;

public class MyQueue {
    private Queue<Task> queue = null;

    public MyQueue() {
        this.queue = new LinkedList<>();
    }

    public synchronized void addTask(int num){
        queue.add(new Task(num));
        notify();
    }
    public synchronized Task getTask() throws InterruptedException {
        if(queue.isEmpty()){
            wait();
        }
        return queue.poll();
    }
}
