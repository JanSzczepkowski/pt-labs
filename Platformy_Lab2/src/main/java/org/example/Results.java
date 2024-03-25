package org.example;

import java.util.HashMap;

public class Results {
    private HashMap<Integer, Boolean> map;

    public Results() {
        this.map = new HashMap<>();
    }

    public synchronized void addResult(int num, boolean isPrime){
        map.put(num, isPrime);
    }

    public void printResults(){
        for (HashMap.Entry<Integer, Boolean> entry : map.entrySet()) {
            int num = entry.getKey();
            boolean isPrime = entry.getValue();
            System.out.println(num + " " + isPrime);
        }
    }
}
