package org.example;

import jakarta.persistence.*;

import java.util.LinkedList;
import java.util.List;

@Entity
@Table(name = "tower")
public class Tower {
    @Id
    @Column(name = "tower")
    private String name;
    @Column(name = "height")
    private int height;
    @OneToMany
    @Column(name = "mages")
    private List<Mage> mages;

    public Tower(String name, int height) {
        this.name = name;
        this.height = height;
        this.mages = new LinkedList<>();
    }

    @Override
    public String toString(){
        return "Tower: " + name + ", Height: " + height;
    }
    public void addToTower(Mage mage){
        mages.add(mage);
    }

    public void deleteFromTower(Mage mage){
        mages.remove(mage);
    }

    public String getName() {
        return name;
    }
}
