#pragma once

#include <QThread>
class PQKitInitThread : public QThread
{
	Q_OBJECT

public:
	PQKitInitThread(QObject* parent = nullptr);
	
signals:
	void signalInitializeKit();

protected:
	void run() override;

};
