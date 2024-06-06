from PIL import Image
import numpy as np

orig_image = Image.open("E:\Projects\ColoringBook\ColoringBook\Assets\Art\crayon.png")
im_size = orig_image.size
print(im_size)
greyshift_image = Image.new(mode="RGB", size=im_size)
orig_data = orig_image.getdata()
#greyshift_data = greyshift_image.getdata()
greyshift_data = []

for x_pix in range(im_size[0]):
    for y_pix in range(im_size[1]):
        pix = orig_data[(im_size[1] * x_pix) + y_pix]
        if (pix[3] == 255):
            greyshift_data.append((pix[0], pix[0], pix[0], 255))
            
        else:
            greyshift_data.append((0, 0, 0, 0))
        print(greyshift_data[(im_size[1] * x_pix) + y_pix])


greyshift_image.putdata(greyshift_data)
greyshift_image.show()
greyshift_image.save("E:\Projects\ColoringBook\ColoringBook\Assets\Art\greyshift_crayon.png", "PNG")
print(type(greyshift_image))
print(dir(greyshift_image))
