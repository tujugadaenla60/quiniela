# quiniela
proyecto para la quiniela Tu Jugada en la 60 (Cliente Bruno Ceriani)

Carpeta Datos: Contiene Backup de Base de Datos SQL Server con estructura y datos de configuración y prueba
Carpeta QuinielaFrontAdmin: Contiene la solución web para la gestión del local, donde se pueden visualizar Clientes, Jugadas, Sorteos
                            Tecnología Microsoft MVC.
Carpeta QuinielaResultsService: Contiene Servicio que verifica cada X tiempo (configurable) los datos de sorteos provistos por DataFactory. 
                                Requiere que la dirección IP del servidor donde se ejecute sea informada a DataFactory para que responda correctamente con la información.
Carpeta Varios: Contiene archivos de resultados de ejemplo de cada Juego, información provista por DataFactory.
