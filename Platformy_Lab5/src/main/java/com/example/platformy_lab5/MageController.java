package com.example.platformy_lab5;

import java.util.Optional;

public class MageController {
    private MageRepository repository;
    public MageController(MageRepository mageRepository){
        this.repository = mageRepository;
    }
    public String find(String name){
        Optional<Mage> mage = repository.find(name);

        if (mage.isPresent()){
            return mage.get().toString();
        }
        return "not found";
    }
    public String delete(String name){
        try{
            repository.delete(name);
            return "done";
        } catch(IllegalArgumentException e){
            return "not found";
        }
    }
    public String save(String name, String level){
        try{
            repository.save(new Mage(name, Integer.parseInt(level)));
        } catch(IllegalArgumentException e){
            return "bad request";
        }

        return "done";
    }
}
