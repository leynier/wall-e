# Programando a Wall-E
###### Segundo Proyecto de Programación. Carrera de Ciencia de la Computación. Universidad de La Habana. 2016 - 2017

#### Introducción

Sobre un mundo rectangular dividido en casillas se ubica un conjunto de objetos. Algunos de estos objetos son robots que pueden ser programados para que efectúen tareas (incluso, competir entre ellos). Los robots pueden desplazarse hacia delante o hacia atrás, o girar 90 grados hacia la izquierda o hacia la derecha. La dirección del robot siempre será alguno de los ángulos ( 0 , 90 , 180 o 270 con respecto al norte). En el terreno puede haber objetos grandes (obstáculos), objetos medianos (movibles) y objetos pequeños (cargables). Si un robot avanza sobre un obstáculo no se mueve, si avanza sobre un objeto movible (siempre que se pueda) lo desplaza y si avanza sobre un objeto pequeño lo recoge (lo almacena en su interior). Si la casilla de enfrente al robot está libre el robot puede soltar (descargar) el objeto en su interior.

El robot tiene un núcleo central de procesamiento que es capaz de ejecutar un conjunto de instrucciones. Además tiene sensores que permiten "percibir" características del ambiente. Por ejemplo, un sensor ultrasónico para determinar distancia o una webcam para identificar el color del objeto enfrente más cercano. Los sensores permiten recibir información del medio y que el robot pueda tomar decisiones de acuerdo a ello.

##### Objetos

Los objetos pueden ser clasificados según distintos atributos (tamaño, forma y color). El tamaño y la forma del objeto determinan si éste puede ser recogido, empujado o si es un obstáculo que impide el avance del robot. El tamaño del objeto frente al robot se puede conocer a través del atributo size y su forma a partir del atributo shape. Además los objetos tienen un color (accesible a través del atributo color) y un código de barra (atributo number). Estos últimos no influyen en la interacción entre el robot y los objetos pero pueden ser utilizados para especificar objetivos del robot (i.e. encontrar la bola negra 8).

Si el objeto es pequeño (no importa la forma) el robot puede agarrarlo (moviéndose para la casilla donde se encuentra dicho objeto). Si el objeto es mediano (o pequeño y el robot ya contiene un objeto pequeño), este puede ser empujado. Las reglas para desplazar un objeto es la siguiente:

* Un robot nunca puede desplazar a otro robot.
* Una caja mediana o pequeña es desplazable siempre y cuando la siguiente casilla está desocupada.
* Una bola grande es desplazable siempre y cuando la siguiente casilla está desocupada.- Una bola mediana o pequeña es desplazable siempre y cuando luego de una serie de bolas pequeñas o medianas contiguas haya una casilla desocupada.
* Una planta nunca es desplazable, con excepción de si es pequeña que el robot la puede recoger.

Si un robot suelta un objeto lo hace a la casilla del frente. Si ya existe un objeto en dicha casilla queda sin efecto la acción.

Para evaluar el comportamiento de los robots en un ambiente controlado se diseñó un lenguaje de programación denominado SINTIME. En este lenguaje cada línea representa una instrucción y cada instrucción comienza por un comando. En el lenguaje se pueden declarar tanto la dinámica del mundo como el programa de cada robot.

#### Lenguaje SINTIME

El lenguaje SINTIME (Single Instruction at a Time) es un lenguaje imperativo con un único ámbito para las variables. En este se pueden declarar rutinas (método, procedimiento) o describir un conjunto de instrucciones a ser ejecutadas por el simulador.

##### Declaraciones

Las declaraciones de rutinas están delimitadas por las instrucciones routine y return. La rutina se define con un nombre con el que será identificada en otras partes del código. El siguiente listado muestra un código con dos rutinas. Las rutinas no se pueden declarar dentro de otras rutinas.

```
routine Accion1
// esto es un comentario
return
routine Accion2
/* esto es otra forma de comentario */
return
```

Las rutinas solo se ejecutan si se invocan mediante el comando `execute`. También pueden ser ejecutadas dependiendo de si cierta condición se cumpla, mediante el comando `execute … if …`

Al terminar la ejecución de una rutina (comando `return`) se debe continuar con la instrucción que le sigue al comando `execute` que inició la ejecución en primer lugar. El lenguaje permite ejecutar cualquier rutina que esté declarada dentro del código, no importa si está antes o después de la declaración actual. **¡La recursión es permitida!**

Otra forma más general de factorización son las bibliotecas. Una biblioteca es un script **SINTIME** que sólo contiene declaraciones de rutina (o incluye otras bibliotecas). Para incluir en el script actual las definiciones de un script biblioteca se utiliza el comando `include <nombre_del_fichero>`. Estos comandos deben ubicarse al principio del código antes de cualquier declaración o instrucción. Por ejemplo:

tools.stm
```
routine Saludar
    message "Hola a todos"
return
```

main.stm
```
include "tools.stm"
execute Saludar
```

Si dos bibliotecas incluidas comparten un mismo nombre para una rutina se debe resolver el conflicto mediante la instrucción `include … as <nombre>` y `execute … from <nombre>`. El siguiente ejemplo muestra una posible resolución del conflicto.

tools1.stm
```
routine Saludar
    message "Hola desde 1"
return
```

tools2.stm
```
routine Saludar
    message "Hola desde 2"
return
```

main.stm
```
include "tools1.stm" as MiModulo1
include "tools2.stm" as MiModulo2
// imprime Hola desde 2
execute Saludar from MiModulo2
```

Si el propio script tiene una rutina con igual nombre que una importada entonces prevalece (sin error de ambigüedad) la rutina local, aunque la rutina importada puede ser accedida mediante la instrucción antes mencionada.

Cualquier punto del código puede ser "etiquetado" mediante un nombre para luego realizar un salto en la ejecución hasta dicha posición. Cada rutina determina un ámbito para las etiquetas, así como el script en sí mismo. Desde una rutina no se puede saltar hacia ninguna etiqueta que esté fuera de su ámbito, ni del código principal se puede saltar a una etiqueta dentro de una rutina. Para etiquetar se utiliza el comando `label <nombre>` y para saltar hacia un punto del código etiquetado se utilizará el comando `goto <nombre>`. En el mismo ámbito no pueden existir dos etiquetas con igual nombre.