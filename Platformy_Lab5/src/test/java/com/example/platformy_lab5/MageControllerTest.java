package com.example.platformy_lab5;


import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.BeforeEach;

import org.mockito.Mockito;

import java.util.Optional;

import static org.junit.jupiter.api.Assertions.*;
import static org.mockito.Mockito.*;

public class MageControllerTest {

    private MageRepository mageRepositoryMock;
    private MageController mageController;

    @BeforeEach
    public void setup() {
        this.mageRepositoryMock = Mockito.mock(MageRepository.class);
        this.mageController = new MageController(mageRepositoryMock);
    }
    @Test
    public void saveNewMage(){
        when(mageRepositoryMock.find("Janek")).thenReturn(Optional.empty());
        assertEquals("done", mageController.save("Janek", "20"));
    }
    @Test
    public void saveExistingMage(){
        when(mageRepositoryMock.find("Janek")).thenReturn(Optional.of(new Mage("Janek", 20)));
        doThrow(new IllegalArgumentException()).when(mageRepositoryMock).save("Janek", 20);
        assertEquals("bad request", mageController.save("Janek", "20"));
    }
    @Test
    public void findNonExistingMage(){
        when(mageRepositoryMock.find("NonExisting")).thenReturn(Optional.empty());
        assertEquals("not found", mageController.find("NonExisting"));
    }
    @Test
    public void findExistingMage(){
        Mage mage = new Mage("Janek", 9);
        when(mageRepositoryMock.find("Janek")).thenReturn(Optional.of(mage));
        assertEquals(mage.toString(), mageController.find("Janek"));
    }

    @Test
    public void deleteExistingMage(){
        Mage mage = new Mage("Janek", 20);
        when(mageRepositoryMock.find("Janek")).thenReturn(Optional.of(mage));
        assertEquals("done", mageController.delete("Janek"));
    }
    @Test
    public void deleteNonExistingMage(){
        when(mageRepositoryMock.find("Janek")).thenReturn(Optional.empty());
        doThrow(new IllegalArgumentException()).when(mageRepositoryMock).delete("Janek");
        assertEquals("not found", mageController.delete("Janek"));
    }


}