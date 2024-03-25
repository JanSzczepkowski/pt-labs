package org.example;

import jakarta.persistence.*;
import org.hibernate.annotations.OnDelete;
import org.hibernate.annotations.OnDeleteAction;

@Entity
@Table(name = "mage")
public class Mage {
    @Id
    @Column(name = "name")
    private String name;

    @Column(name = "level")
    private int level;
    @ManyToOne
    @OnDelete(action = OnDeleteAction.CASCADE)
    private Tower tower;


    public Mage(String name, int level, Tower tower) {
        this.name = name;
        this.level = level;
        this.tower = tower;
        tower.addToTower(this);
    }

    @Override
    public String toString(){
        return "Mage: " + name + ", Level: " + level + ", Tower: " + tower.getName();
    }

    public Tower getTower() {
        return tower;
    }
}
