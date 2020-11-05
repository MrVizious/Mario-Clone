# PEC 2 - Un juego de plataformas

Este es un trabajo realizado para la asignatura de Programación 2D del máster en Diseño y Programación de Videojuegos de la UOC.

A continuación se explicarán algunos de los elementos claves del trabajo.

## Fidelidad al juego original

Como reto personal, en este trabajo he intentado realizar el juego de la manera más cercana posible a las físicas del original. Para ello, y tras investigar por la red, me topé con [este post](https://web.archive.org/web/20130807122227/http://i276.photobucket.com/albums/kk21/jdaster64/smb_playerphysics.png) en el que se detallaba cómo funcionaban las físicas en el juego de la NES. La misma imagen se encuentra también en [esta ruta local](/README_Data/smb_playerphysics.png).

El problema reside en que estos cálculos están en bloques, píxeles, subpíxeles, subsubpíxeles y subsubsubpíxeles por frame, tomando como referencia 60 frames por segundo. Por suerte, un simple programa que sirva como traductor entre estas medidas y unidades por segundo nos servirá, dado que la velocidad en Unity se calcula de esa manera según [esta página de la documentación](https://docs.unity3d.com/ScriptReference/Rigidbody-velocity.html), en una nota al pie.

```csharp
//Change the values to match the ones found in the physics breakdown
float block = 0, pixel = 0, spixel = 0, sspixel = 0, ssspixel = 0;
		
float unitsMoved = block + (pixel/16) + (spixel/(16*16)) + (sspixel/(16*16*16)) + (sspixel/(16*16*16*16));

Console.WriteLine("Units moved in a frame: " + unitsMoved);
Console.WriteLine("Units moved in a second: " + (unitsMoved*60));
```

Con este script hecho a propósito para este trabajo, sacaremos las medidas que necesitamos.

Por ejemplo, comenzaremos sacando la velocidad máxima de Mario al andar sobre tierra. Según el post esta velocidad es 01900 (1 píxel y 9 subpíxeles) por cada frame. Al hacer el cálculo, nos sale un resultado de 5.761719 unidades por segundo, muy similar a la velocidad que se calcula en [esta otra fuente](https://explodingrabbit.com/game-units-577/), que sale a 5.625 unidades por segundo. Para tener unas medidas más coherentes entre ellas a lo largo del trabajo, usaremos las medidas que calculemos nosotros.

https://blog.hamaluik.ca/posts/super-mario-world-physics/