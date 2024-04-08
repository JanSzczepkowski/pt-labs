package com.example.platformy_lab5;

import org.junit.Assert;
import org.junit.Test;

import java.util.Optional;

import static org.junit.Assert.assertEquals;
import static org.junit.Assert.assertFalse;


public class MageRepositoryTest {

    private MageRepository mageRepository;



    @Test
    public void testFindExistingMage() {
        mageRepository = new MageRepository();

        mageRepository.save("Jan", 7);

        Optional<Mage> foundMage = mageRepository.find("Jan");
        assertEquals("Jan", foundMage.get().getName());
    }

    @Test
    public void testFindNonExistingMage() {
        mageRepository = new MageRepository();
        Optional<Mage> foundMage = mageRepository.find("NonExisting");
        assertFalse(foundMage.isPresent());
    }

    @Test
    public void testSave() {
        mageRepository = new MageRepository();
        mageRepository.save("Kamil", 5);
        Optional<Mage> foundMage = mageRepository.find("Kamil");
        assertEquals("Kamil", foundMage.get().getName());
    }

    @Test
    public void testDeleteExistingMage() {
        mageRepository = new MageRepository();
        mageRepository.save("Adam", 3);

        mageRepository.delete("Adam");
        Optional<Mage> foundMage = mageRepository.find("Adam");
        assertFalse(foundMage.isPresent());
    }

    @Test
    public void testDeleteNonExistingMage() {
        mageRepository = new MageRepository();
        Assert.assertThrows(IllegalArgumentException.class, () -> mageRepository.delete("NonExisting"));
    }
}
