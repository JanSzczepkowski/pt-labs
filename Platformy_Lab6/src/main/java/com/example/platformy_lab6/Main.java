package com.example.platformy_lab6;

import org.apache.commons.lang3.tuple.Pair;

import javax.imageio.ImageIO;
import java.awt.*;
import java.awt.image.BufferedImage;
import java.io.BufferedReader;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.util.List;
import java.util.concurrent.ExecutionException;
import java.util.concurrent.ForkJoinPool;
import java.util.stream.Collectors;
import java.util.stream.Stream;

public class Main {
    public static void main(String[] args) throws ExecutionException, InterruptedException {
        if(args.length < 3){
            System.out.println("not enough arguments provided");
            return;
        }
        Path source = Path.of(args[0]);
        Path dest = Path.of(args[1]);
        int threads = Integer.parseInt(args[2]);
        List<Path> files;
        try {
            Stream<Path> stream = Files.list(source);
            files = stream.toList();
        } catch (IOException e) {
            throw new RuntimeException(e);
        }

        long time = System.currentTimeMillis();

        ForkJoinPool forks = new ForkJoinPool(threads);

        forks.submit(()->
                files.parallelStream()
                        .forEach(file -> changeFile(dest, file))
        ).get();
        System.out.println(((System.currentTimeMillis() - time) / 1000) + "s");
    }

    public static void changeFile(Path dest, Path file){
        try{
            BufferedImage image = ImageIO.read(file.toFile());
            String name = String.valueOf(file.getFileName());

            BufferedImage newImage = new BufferedImage(image.getWidth(), image.getHeight(), image.getType());

            for (int i = 0; i < image.getWidth(); i++) {
                for (int j = 0; j < image.getHeight(); j++) {
                    int rgb = image.getRGB(i, j);
                    Color color = new Color(rgb);

                    int red = color.getRed();
                    int blue = color.getBlue();
                    int green = color.getGreen();

                    Color outColor = new Color(red, blue, green);

                    newImage.setRGB(i, j, outColor.getRGB());
                }
            }
            Pair<String, BufferedImage> newPair = Pair.of(name, newImage);
            ImageIO.write(newPair.getRight(), "jpg", Path.of(String.valueOf(dest), newPair.getLeft()).toFile());
        } catch (IOException e){
            e.printStackTrace();
        }

    }
}
