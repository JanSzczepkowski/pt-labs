package com.example.platformy_lab5;

import org.junit.Assert;
import org.junit.Test;

import java.util.Optional;


public class MageRepositoryTest {

    private MageRepository mageRepository;



    @Test
    public void testFindExistingMage() {
        mageRepository = new MageRepository();
        Mage mage = new Mage("Jan", 7);
        mageRepository.save(mage);

        Optional<Mage> foundMage = mageRepository.find("Jan");
        Assert.assertTrue(foundMage.isPresent());
        Assert.assertEquals("Jan", foundMage.get().getName());
        Assert.assertEquals(7, foundMage.get().getLevel());
    }

    @Test
    public void testFindNonExistingMage() {
        mageRepository = new MageRepository();
        Optional<Mage> foundMage = mageRepository.find("NonExisting");
        Assert.assertFalse(foundMage.isPresent());
    }

    @Test
    public void testSave() {
        mageRepository = new MageRepository();
        Mage mage = new Mage("Kamil", 5);
        mageRepository.save(mage);

        Optional<Mage> foundMage = mageRepository.find("Kamil");
        Assert.assertTrue(foundMage.isPresent());
        Assert.assertEquals("Kamil", foundMage.get().getName());
        Assert.assertEquals(5, foundMage.get().getLevel());
    }

    @Test
    public void testDeleteExistingMage() {
        mageRepository = new MageRepository();
        Mage mage = new Mage("Adam", 3);
        mageRepository.save(mage);

        mageRepository.delete("Adam");
        Optional<Mage> foundMage = mageRepository.find("Adam");
        Assert.assertFalse(foundMage.isPresent());
    }

    @Test
    public void testDeleteNonExistingMage() {
        mageRepository = new MageRepository();
        Assert.assertThrows(IllegalArgumentException.class, () -> mageRepository.delete("NonExisting"));
    }
}
