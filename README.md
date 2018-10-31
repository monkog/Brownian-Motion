# Brownian Motion
[![Build status](https://ci.appveyor.com/api/projects/status/e664l8464e5xpt3a?svg=true)](https://ci.appveyor.com/project/monkog/brownian-motion)

Brownian Motion (pedesis) describes the random movement of small particles suspended in a liquid or a gas due to their collisions with other molecules or atoms. 

![](./.Docs/BrownianMotion.JPG)

## Fractional Brownian Motion
Fractional Brownian Motion is a generalization of Brownian Motion. It's a Gaussian random function that is described by the following covariance function:


![equation](http://latex.codecogs.com/gif.latex?%5Cdpi%7B120%7D%20%5C%5CE%5BB_H%28t%29%20B_H%20%28s%29%5D%3D%5Ctfrac%7B1%7D%7B2%7D%20%28%7Ct%7C%5E%7B2H%7D&plus;%7Cs%7C%5E%7B2H%7D-%7Ct-s%7C%5E%7B2H%7D%29)

Where _H ϵ (0,1)_ is the Hurst parameter.
For _H = 1/2_ the Fractional Brownian Motion is a Brownian Motion.

![](./.Docs/FractionalBrownianMotionH09N13.JPG)

## :link: Useful links
* [Brownian Motion](http://galileo.phys.virginia.edu/classes/304/brownian.pdf)
* [Fractional Brownian Motion](https://users.math.yale.edu/~bbm3/web_pdfs/052fractionalBrownianMotions.pdf)
