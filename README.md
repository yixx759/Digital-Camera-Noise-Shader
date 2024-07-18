# Digital-Camera-Noise-Shader
Simulate 2000's digital camera noise in unity

A big characteristic of digital cameras specifically from the 2000s is the presence of noise.
This is due to much smaller mega pixel sensors.

![dslr-3rd-ed-nov12-print-text_page_057_image_0002_reference](https://github.com/user-attachments/assets/6cc0f538-f62c-42e0-bd27-c58a6f2f856d)

![620c02da6b65b59cdd65d987_Bill Maynard - Mink zoom](https://github.com/user-attachments/assets/1e669cc0-3d15-4d0b-9a01-4fbcb6e921c5)

![600px-Noise-wall](https://github.com/user-attachments/assets/ca069f25-b317-44e1-a7e3-be93f0cf65f5)

![Highimgnoise (1)](https://github.com/user-attachments/assets/481e0c26-16ae-40e4-ae24-85b34ad09377)

This noise seems to be made up of a part that affects brightness luminance noise.
Another which effects colour chroma noise.

https://www.cambridgeincolour.com/tutorials/image-noise-2.htm

Small sensor size causes shot noise which follows a poisson distribution.
https://en.wikipedia.org/wiki/Image_noise

The method I used is a additive white gaussian distribution to add a non signal dependant noise. 
Then used a poisson distribution dependant on the luminance of the pixel for the signal dependant noise.
https://ieeexplore.ieee.org/abstract/document/4623175

![git2](https://github.com/user-attachments/assets/bbbbe51b-040b-4c4f-8469-37b8596a12d0)
![Git1](https://github.com/user-attachments/assets/b4dbef24-9379-4f2f-a528-f16011200ed1)


Gaussian random sample was taken from here. Using box-muller method.
https://github.com/thomas-moulard/gazebo-deb/blob/master/media/materials/programs/camera_noise_gaussian_fs.glsl

Then for possion portion an approximation is used by sampling a normal distribution but with a mean of pixel luminace.
Then for SD of the luminance multiplied by a controllabel paramter to control intensity.


For chroma noise as seen here.
https://www.cambridgeincolour.com/tutorials/image-noise-2.htm
Each color channel has its own unique variation. This noise looks very similar to perlin.

R G B

![noise_epsonISO400r-crop](https://github.com/user-attachments/assets/0f7fd491-f299-4309-8464-8b6cf4a25f1e)
 ![noise_epsonISO400g-crop](https://github.com/user-attachments/assets/645c6c07-6da1-4cdd-839e-83c5b4c428f3)
 ![noise_epsonISO400b-crop](https://github.com/user-attachments/assets/9fa78249-2099-46e7-b00b-1e473d395f3b)

My Noise

R G B

![rc](https://github.com/user-attachments/assets/d66d3cb1-37e2-4628-bf16-afc14eceffa9)
![gc](https://github.com/user-attachments/assets/f31f6dff-8d76-4325-b2ad-46c7658a6349)
![bc](https://github.com/user-attachments/assets/8ad12eb2-4623-43cd-9c85-3ddd1de1c915)


The results from each combined is 

![noise_epsonISO400-crop](https://github.com/user-attachments/assets/eaf21454-48e7-47bb-b979-7eba714c1979)
![c](https://github.com/user-attachments/assets/35ea0ddc-41e9-42fe-a0d5-d612c31cce99)

This works well enough.

After multiplying this new texture by the luminace noise value we got it results in an image like this.

![r1](https://github.com/user-attachments/assets/e78f170d-e5b5-4148-95f9-8be3da3ade5c)
![r2](https://github.com/user-attachments/assets/32c2fdae-7d5a-40a0-b32e-188ad7ba419e)
![r3](https://github.com/user-attachments/assets/c49febb2-c505-4087-8a11-d1044ace1da2)

No Noise - My Noise - Real Noise

Note for accuracy you may want to make gaussian stationary and not dependant on a random seed that changes every frame.
