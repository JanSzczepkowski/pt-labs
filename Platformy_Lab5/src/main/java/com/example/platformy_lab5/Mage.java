package com.example.platformy_lab5;

public class Mage {
    private String name;
    private int level;
    public Mage(String name, int level){
        this.name = name;
        this.level = level;
    }
    @Override
    public String toString(){
        return "name - " + this.name + ", level - " + this.level;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public int getLevel() {
        return level;
    }

    public void setLevel(int level) {
        this.level = level;
    }
}
