
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;



public class Pedido
{
    public int Id { get; set; }

    public DateTime Fecha { get; set; } = DateTime.Now;

    [Required]
    public string IdUsuario { get; set; }
    public decimal MontoTotal { get; set; }

    public string Estado { get; set; } = "Pendiente";

}

