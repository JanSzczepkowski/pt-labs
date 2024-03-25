package org.example;

import java.util.*;

public class Main {
    public static void main(String[] args) {
        if(args.length > 0) {
            String sortOption = args[0];
            MageSetFactory newMageSet = new MageSetFactory(sortOption);
            newMageSet.generateMags();

            MageStatistics newMageStatistics = new MageStatistics(newMageSet.getMags(), sortOption);

            System.out.println("\n Mage recursive output: \n");
            newMageSet.printMageSet();
            System.out.println("\n Mage statictics: \n");
            newMageStatistics.printStatistics();
        } else {
            System.out.println("No args given.");
        }

    }
}