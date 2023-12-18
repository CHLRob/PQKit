#include "PQKitInitThread.h"

PQKitInitThread::PQKitInitThread(QObject *parent)
{

}


void PQKitInitThread::run()
{
	emit signalInitializeKit();
}
