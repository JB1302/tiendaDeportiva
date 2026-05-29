package com.Pawtify.pawtify.repository;

import com.Pawtify.pawtify.domain.Carrito;
import com.Pawtify.pawtify.domain.CarritoItem;
import com.Pawtify.pawtify.domain.Producto;
import org.springframework.data.jpa.repository.JpaRepository;
import java.util.List;
import java.util.Optional;

public interface CarritoItemRepository extends JpaRepository<CarritoItem, Long> {
    List<CarritoItem> findByCarrito(Carrito carrito);
    Optional<CarritoItem> findByCarritoAndProducto(Carrito carrito, Producto producto);
}
