# Piedra Papel Tijera Multihilo

Este proyecto implementa un torneo de Piedra, Papel o Tijera utilizando programación concurrente en C#. Cada jugador es representado por un hilo, y el torneo sigue un formato de eliminación directa hasta determinar un único ganador.

## Características

- **Multihilo**: Cada jugador es un hilo independiente que participa en el torneo.
- **Eliminación directa**: Los jugadores compiten en rondas, reduciéndose el número de participantes a la mitad en cada ronda.
- **Simulación aleatoria**: Las elecciones de Piedra, Papel o Tijera se realizan de manera aleatoria.



## Estructura del Código

### Clases y Métodos Principales

1. **Clase `PiedraPapelTijera`**: Contiene el punto de entrada (`Main`) y los métodos para manejar el torneo.

2. **Método `ManageGame`**: Gestiona el flujo del torneo:
    - Organiza a los jugadores en parejas.
    - Ejecuta cada partida en un hilo independiente.
    - Determina los ganadores de cada ronda.

3. **Método `PlayMatch`**: Simula una partida entre dos jugadores:
    - Los jugadores eligen de forma aleatoria entre "Piedra", "Papel" o "Tijera".
    - Determina el ganador según las reglas tradicionales del juego.

4. **Método `GenerateRandomName`**: Genera una lista de nombres aleatorios para los jugadores.
