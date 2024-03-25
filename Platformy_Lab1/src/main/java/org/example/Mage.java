package org.example;
import java.util.Collections;
import java.util.HashSet;
import java.util.Objects;
import java.util.Set;
public class Mage implements Comparable<Mage> {
    private String name;
    private int level;
    private double power;
    private Set<Mage> apprentices;

    public Mage(String name, int level, double power) {
        this.name = name;
        this.level = level;
        this.power = power;
        this.apprentices = new HashSet<>();
    }

    public void addApprentice(Mage mage){
        apprentices.add(mage);
    }

    public String getName() {
        return name;
    }

    public int getLevel() {
        return level;
    }

    public double getPower() {
        return power;
    }

    public Set<Mage> getApprentices() {
        return apprentices;
    }

    public void setName(String name) {
        this.name = name;
    }

    public void setLevel(int level) {
        this.level = level;
    }

    public void setPower(double power) {
        this.power = power;
    }

    public int countApprentices() {
        return apprentices.size();
    }

    @Override
    public int compareTo(Mage other) {
        return this.name.compareTo(other.name);
    }

    @Override
    public String toString() {
        return "Mage: { name: " + name + ", level: " + level + ", power: " + power + " }";
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;
        Mage mage = (Mage) o;
        return level == mage.level && Double.compare(power, mage.power) == 0 && Objects.equals(name, mage.name) && Objects.equals(apprentices, mage.apprentices);
    }

    @Override
    public int hashCode() {
        return Objects.hash(name, level, power, apprentices);
    }
}
