package com.example.platformy_lab5;

import java.util.Scanner;

public class Main {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);

        MageRepository repo = new MageRepository();
        MageController controller = new MageController(repo);
        while(true){
            System.out.println("Enter add/delete/find/exit");
            String input = "";
            if(scanner.hasNextLine()){
                input = scanner.nextLine();
            }
            if(input.equals("exit")){
                return;
            } else if(input.equals("add")){
                System.out.println("Enter mags name and level");
                String name = "", level = "";
                if(scanner.hasNextLine()){
                    name = scanner.nextLine();
                }
                if(scanner.hasNextLine()){
                    level = scanner.nextLine();
                }
                System.out.println(controller.save(name, level));
            } else if(input.equals("find")){
                System.out.println("Enter mags name");
                String name = scanner.nextLine();
                System.out.println(controller.find(name));
            } else if (input.equals("delete")){
                System.out.println("Enter mags name");
                String name = scanner.nextLine();
                System.out.println(controller.delete(name));
            }
        }
    }
}
