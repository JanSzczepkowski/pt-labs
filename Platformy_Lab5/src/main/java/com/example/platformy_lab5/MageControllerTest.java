package com.example.platformy_lab5;

import org.junit.Assert;
import org.junit.Test;


public class MageControllerTest {
    @Test
    public void testFind(){
        MageRepository repo = new MageRepository();
        MageController controller = new MageController(repo);
        controller.save("Jan", "7");
        Assert.assertEquals("name - Jan, level - 7" ,controller.find("Jan"));
        Assert.assertEquals("not found", controller.find("Janek"));
    }

    @Test
    public void testDelete(){
        MageRepository repo = new MageRepository();
        MageController controller = new MageController(repo);
        controller.save("Jan", "7");
        Assert.assertEquals("done", controller.delete("Jan"));
        Assert.assertEquals("not found", controller.delete("Jan"));
    }

    @Test
    public void testSave(){
        MageRepository repo = new MageRepository();
        MageController controller = new MageController(repo);
        Assert.assertEquals("done", controller.save("Jan", "7"));
        Assert.assertEquals("bad request", controller.save("Jan", "9"));
    }

}
