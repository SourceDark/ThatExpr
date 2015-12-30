
CmpImage.so: CmpStrategy.o CmpPic.o CmpGIF.o Expression.o ExpressionComparer.o CmpImage.o
#	g++ -shared *.o -o CmpImage.so -L$(parent)/lib/cximage -lCxImage -ljasper -ljpeg -ltiff -lj2k -ljbig -lpng -lzlib
	g++ -shared *.o -o CmpImage.so $(CFLAGS) $(LDFLAGS) $(LIBS)
current:=$(shell pwd)
parent:=$(shell dirname $(current))
LDFLAGS:=$(LDFLAGS) -L$(parent)/lib/cximage
CFLAGS:=$(CFLAGS) -I../include/cximage -I$(JAVA_HOME)/include -I$(JAVA_HOME)/include/linux
LIBS:=$(LIBS) -lCxImage -ljasper -ljpeg -ltiff -lj2k -ljbig -lpng -lzlib -lstdc++ -lm
CC:=gcc
CmpStrategy.o:CmpStrategy.h CmpStrategy.cpp
	gcc -c -fpic CmpStrategy.cpp $(CFLAGS) $(LDFLAGS) $(LIBS)


CmpPic.o:
	gcc -c -fpic CmpPic.cpp $(CFLAGS) $(LDFLAGS) $(LIBS)


CmpGIF.o:CmpGIF.h CmpGIF.cpp
	gcc -c -fpic CmpGIF.cpp $(CFLAGS) $(LDFLAGS) $(LIBS)
Expression.o:Expression.cpp Expression.h
	gcc -c -fpic Expression.cpp $(CFLAGS) $(LDFLAGS) $(LIBS)

ExpressionComparer.o:ExpressionComparer.cpp ExpressionComparer.h
	gcc -c -fpic ExpressionComparer.cpp $(CFLAGS) $(LDFLAGS) $(LIBS)
CmpImage.o:CmpImage.cpp CmpImage.h
	gcc -c -fpic CmpImage.cpp $(CFLAGS) $(LDFLAGS) $(LIBS)


.PHONY:clean
clean:
	rm *.o
	rm CmpImage.so
