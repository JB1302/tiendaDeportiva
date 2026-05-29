package com.Pawtify.pawtify.domain;

import jakarta.persistence.*;
import lombok.Data;

@Data
@Entity
public class CarritoItem {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;
    
    @ManyToOne
    @JoinColumn(name = "carrito_id", nullable = false)
    private Carrito carrito;
    
    @ManyToOne
    @JoinColumn(name = "producto_id", nullable = false)
    private Producto producto;
    
    private int cantidad;
    
    public double getSubtotal() {
        return producto.getPrecio() * cantidad;
    }
}
