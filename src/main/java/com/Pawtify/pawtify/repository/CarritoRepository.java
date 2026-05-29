package com.Pawtify.pawtify.repository;

import com.Pawtify.pawtify.domain.Carrito;
import com.Pawtify.pawtify.domain.User;
import org.springframework.data.jpa.repository.JpaRepository;
import java.util.Optional;

public interface CarritoRepository extends JpaRepository<Carrito, Long> {
    Optional<Carrito> findByUsuario(User usuario);
}
