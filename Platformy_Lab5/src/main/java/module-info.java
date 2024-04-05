module com.example.platformy_lab5 {
    requires javafx.controls;
    requires javafx.fxml;
    requires junit;


    opens com.example.platformy_lab5 to javafx.fxml;
    exports com.example.platformy_lab5;
}