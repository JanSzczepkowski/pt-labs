package com.example.platformy_lab5;

import java.util.ArrayList;
import java.util.Collection;
import java.util.Optional;

public class MageRepository {
    private Collection<Mage> collection;

    public MageRepository(){
        this.collection = new ArrayList<>();
    }

    public Optional<Mage> find(String name){
        for (Mage mage : collection){
            if(mage.getName().equals(name)){
                return Optional.of(mage);
            }
        }
        return Optional.empty();
    }
    public void delete(String name){
        Optional<Mage> mage = this.find(name);
        if(mage.isPresent()){
            Mage mageToDelete = mage.get();
            collection.remove(mageToDelete);

            return;
        }
        throw new IllegalArgumentException("Mage not found");
    }
    public void save(String name , int level ){ //String name, int level
        Mage mage = new Mage(name, level);
        Optional<Mage> mageToAdd = this.find(name);
        if(mageToAdd.isPresent()){
            throw new IllegalArgumentException("Mage named " + name + " already in repository");
        } else {
            collection.add(mage);
        }
    }
}
