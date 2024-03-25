package org.example;

import org.hibernate.Transaction;
import org.hibernate.Session;
import org.hibernate.query.Query;

import java.util.List;
import java.util.Scanner;

//TIP To <b>Run</b> code, press <shortcut actionId="Run"/> or
// click the <icon src="AllIcons.Actions.Execute"/> icon in the gutter.
public class Main {
    public static void main(String[] args) {
        Tower tower1 = new Tower("Tower 1", 50);
        Mage mage = new Mage("Marcin", 77, tower1);
        Transaction transaction = null;
        try(Session session = HibernateUtil.getSessionFactory().openSession()){
            System.out.println("elo");
            transaction = session.beginTransaction();

            session.persist(tower1);
            session.persist(mage);
            transaction.commit();
            Scanner scanner = new Scanner(System.in);
            while(true){
                System.out.println("What do you want to do? Type show/add/delete/showParam");
                transaction = session.beginTransaction();
                String input = "";
                if(scanner.hasNextLine()){
                    input = scanner.nextLine();
                }
                if (input.equals("show")){
                    System.out.println("What do you want to see? Type mage/tower");
                    String option = scanner.nextLine();
                    if(option.equals("mage")){
                        System.out.println("Enter mages name, level and the tower he belongs to");
                        Query<Mage> query = session.createQuery("FROM Mage", Mage.class);
                        List<Mage> mages = query.list();
                        System.out.println("All mages:");
                        for (Mage mageToPrint : mages) {
                            System.out.println(mageToPrint.toString());
                        }
                    } else if(option.equals("tower")){
                        System.out.println("Enter towers name and height");
                        Query<Tower> query = session.createQuery("FROM Tower", Tower.class);

                        List<Tower> towers = query.list();

                        System.out.println("All towers:");
                        for (Tower tower : towers) {
                            System.out.println(tower.toString());
                        }
                    } else {
                        System.out.println("wrong option");
                    }
                } else if(input.equals("add")){
                    System.out.println("What do you want to add? Type mage/tower");
                    String option = scanner.nextLine();
                    if (option.equals("mage")){
                        System.out.println("Enter mages name, level and tower name");
                        String name = scanner.nextLine();
                        int level = Integer.parseInt(scanner.nextLine());
                        String towerName = scanner.nextLine();

                        Query<Tower> towerQuery = session.createQuery("FROM Tower WHERE name = :towerName", Tower.class);
                        towerQuery.setParameter("towerName", towerName);
                        Tower tower = towerQuery.uniqueResult();
                        System.out.println(tower.toString());
                        if (tower != null) {
                            Mage mageToAdd = new Mage(name, level, tower);
                            session.persist(mageToAdd);
                            System.out.println("Mage added!");

                        } else {
                            System.out.println("Tower with name " + towerName + " does not exist!");
                        }
                    } else if(option.equals("tower")){
                        System.out.println("Enter towers name and its height");
                        String name = scanner.nextLine();
                        int height = Integer.parseInt(scanner.nextLine());
                        Tower towerToAdd = new Tower(name, height);
                        session.persist(towerToAdd);
                        System.out.println("Tower added!");

                    } else {
                        System.out.println("wrong option");
                    }
                } else if (input.equals("delete")){
                    System.out.println("Choose what you want do delete, type mage/tower");
                    String option = scanner.nextLine();
                    if (option.equals("mage")){
                        System.out.println("Enter mags name");
                        String magsName = scanner.nextLine();

                        Query<Mage> query = session.createQuery("FROM Mage WHERE name = :mageName", Mage.class);
                        query.setParameter("mageName", magsName);
                        Mage mageToDelete = query.uniqueResult();

                        System.out.println(mageToDelete.toString());
                        if(mageToDelete != null){
                            Tower tower = mageToDelete.getTower();
                            session.delete(mageToDelete);
                            tower.deleteFromTower(mageToDelete);
                            session.update(tower);
                            System.out.println("Mage deleted");
                        } else {
                            System.out.println("Mage not found");
                        }
                    } else if (option.equals("tower")){
                        System.out.println("Enter towers name");
                        String towersName = scanner.nextLine();
                        Query<Tower> towerQuery = session.createQuery("FROM Tower WHERE name = :towerName", Tower.class);
                        towerQuery.setParameter("towerName", towersName);
                        Tower tower = towerQuery.uniqueResult();
                        session.delete(tower);
                    } else {
                        System.out.println("wrong option");
                    }
                } else if (input.equals("showParam")){
                    System.out.println("Enter towers height. You will see towers shorter than given height");
                    int height = Integer.parseInt(scanner.nextLine());

                    Query<Tower> query = session.createQuery("FROM Tower WHERE height < :towerHeight", Tower.class);
                    query.setParameter("towerHeight", height);
                    List<Tower> shorterTowers = query.list();


                    if (shorterTowers.isEmpty()) {
                        System.out.println("No towers shorter than " + height);
                    } else {
                        System.out.println("Towers shorter than " + height + ":");
                        for (Tower tower : shorterTowers) {
                            System.out.println(tower);
                        }
                    }
                } else if (input.equals("exit")){
                    break;
                }
                transaction.commit();
            }
        } catch (Exception e){
            e.printStackTrace();
        }
    }
}