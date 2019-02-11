#define WIN_X86
//#define LINUX_X86

#include "Core.h"


int _cdecl main() {
	Core rtServer;

	if (rtServer.InitWinsock == 1) {
		std::cout << "Shutdown rtCore" << std::endl;
		return 1;
	}

	return 0;
}