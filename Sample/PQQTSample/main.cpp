#import "RPC.tlb" no_namespace, named_guids, raw_interfaces_only, raw_native_types

#include "MainWindow.h"
#include <QApplication>


int main(int argc, char *argv[])
{
    QApplication app(argc, argv);
    MainWindow *wWindow = new MainWindow;
	wWindow->setWindowTitle("PQKit QT Sample");
	wWindow->hide();
	
	app.processEvents();
	
	return 0;
}
