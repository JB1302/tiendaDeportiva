package com.Pawtify.pawtify.service;

import com.Pawtify.pawtify.domain.*;
import com.Pawtify.pawtify.repository.*;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;
import java.util.List;

@Service
public class CarritoService {
    @Autowired private CarritoRepository carritoRepo;
    @Autowired private CarritoItemRepository itemRepo;
    @Autowired private ProductoRepository productoRepo;
    @Autowired private UserRepository userRepo;

    @Transactional
    public void agregarProducto(Long productoId, int cantidad, String username) {
        User usuario = userRepo.findByEmail(username).orElseThrow();
        Producto producto = productoRepo.findById(productoId).orElseThrow();
        
        Carrito carrito = carritoRepo.findByUsuario(usuario)
            .orElseGet(() -> {
                Carrito nuevo = new Carrito();
                nuevo.setUsuario(usuario);
                return carritoRepo.save(nuevo);
            });

        itemRepo.findByCarritoAndProducto(carrito, producto)
            .ifPresentOrElse(
                item -> item.setCantidad(item.getCantidad() + cantidad),
                () -> {
                    CarritoItem item = new CarritoItem();
                    item.setCarrito(carrito);
                    item.setProducto(producto);
                    item.setCantidad(cantidad);
                    itemRepo.save(item);
                }
            );
    }

    @Transactional(readOnly = true)
    public List<CarritoItem> obtenerItems(String username) {
        User usuario = userRepo.findByEmail(username).orElseThrow();
        return carritoRepo.findByUsuario(usuario)
            .map(carrito -> itemRepo.findByCarrito(carrito))
            .orElse(List.of());
    }
}   
