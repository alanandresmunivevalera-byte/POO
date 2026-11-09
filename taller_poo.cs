using System;

public class HelloWorld
{
    public class HabitacionHotel
    {
        private int numero;
        private string tipo;
        private double precioPorNoche;
        private bool disponible;
        private string clienteActual;

        public HabitacionHotel(int numero, string tipo, double precioPorNoche)
        {
            this.numero = numero;
            Tipo = tipo;
            PrecioPorNoche = precioPorNoche;
            disponible = true;
            clienteActual = "";
        }

        public int Numero
        {
            get { return numero; }
        }

        public string Tipo
        {
            get { return tipo; }
            set
            {
                if (value == "Individual" || value == "Doble" || value == "Suite")
                    tipo = value;
                else
                    throw new ArgumentException("Tipo de habitación no válido");
            }
        }

        public double PrecioPorNoche
        {
            get { return precioPorNoche; }
            set
            {
                if (value > 0)
                    precioPorNoche = value;
                else
                    throw new ArgumentException("El precio debe ser mayor a cero");
            }
        }

        public bool Disponible
        {
            get { return disponible; }
        }

        public string ClienteActual
        {
            get { return clienteActual; }
        }

        public double Reservar(string nombreCliente, int noches)
        {
            if (!disponible)
                return -1;

            if (string.IsNullOrWhiteSpace(nombreCliente) || noches <= 0)
                throw new ArgumentException("Datos de reserva no válidos");

            disponible = false;
            clienteActual = nombreCliente;

            return precioPorNoche * noches;
        }

        public bool Liberar()
        {
            if (disponible)
                return false;

            disponible = true;
            clienteActual = "";

            return true;
        }

        public string MostrarInfo()
        {
            string estado = disponible ? "Disponible" : "Ocupada";
            string cliente = disponible ? "Ninguno" : clienteActual;

            return $"Número: {numero}, Tipo: {tipo}, " +
                   $"Precio por noche: {precioPorNoche:C}, " +
                   $"Estado: {estado}, Cliente: {cliente}";
        }

        public void AplicarDescuento(int porcentaje)
        {
            if (porcentaje < 0 || porcentaje > 100)
                throw new ArgumentException("Porcentaje de descuento no válido");

            precioPorNoche = precioPorNoche * (1 - porcentaje / 100.0);
        }
    }

    public static void Main(string[] args)
    {
        HabitacionHotel habitacion1 =
            new HabitacionHotel(101, "Individual", 50000);

        HabitacionHotel habitacion2 =
            new HabitacionHotel(202, "Doble", 80000);

        HabitacionHotel habitacion3 =
            new HabitacionHotel(303, "Suite", 150000);

        Console.WriteLine("ESTADO INICIAL");
        Console.WriteLine(habitacion1.MostrarInfo());
        Console.WriteLine(habitacion2.MostrarInfo());
        Console.WriteLine(habitacion3.MostrarInfo());

        Console.WriteLine("\nRESERVAS");

        double costo = habitacion1.Reservar("Samuel Montiel", 3);
        Console.WriteLine($"Costo de Samuel Montiel: {costo:C}");

        costo = habitacion2.Reservar("hayder reyes", 2);
        Console.WriteLine($"Costo de hayder reyes : {costo:C}");

        costo = habitacion1.Reservar("Otro Cliente", 2);
        Console.WriteLine(costo == -1
            ? "La habitación 101 está ocupada"
            : $"Costo de la reserva: {costo:C}");

        bool resultado = habitacion1.Liberar();
        Console.WriteLine(resultado
            ? "Habitación 101 liberada"
            : "La habitación 101 ya estaba disponible");

        habitacion3.AplicarDescuento(10);
        Console.WriteLine("Descuento del 10% aplicado a la habitación 303");

        costo = habitacion1.Reservar("Samuel Montiel", 1);
        Console.WriteLine($"Costo de Samuel Montiel: {costo:C}");

        costo = habitacion3.Reservar("dunga rafael", 2);
        Console.WriteLine($"Costo de dunga rafael : {costo:C}");

        Console.WriteLine("\nESTADO FINAL");
        Console.WriteLine(habitacion1.MostrarInfo());
        Console.WriteLine(habitacion2.MostrarInfo());
        Console.WriteLine(habitacion3.MostrarInfo());

        int ocupadas = 0;

        if (!habitacion1.Disponible)
            ocupadas++;

        if (!habitacion2.Disponible)
            ocupadas++;

        if (!habitacion3.Disponible)
            ocupadas++;

        Console.WriteLine($"\nTotal de habitaciones ocupadas: {ocupadas}");
    }
}

