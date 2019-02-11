#include "Core.h"

Core::Core() {
	
}

int Core::InitWinsock() {
	iResult = WSAStartup(MAKEWORD(2, 2), &wsaData); // Initiate use of WS2_32.dll, MAKEWORD select Winsock version, here 2.2
	if (iResult != 0) {
		std::cout << "WSAStartup failed: " << iResult << std::endl;
		return 1;
	}

	return 0;
}