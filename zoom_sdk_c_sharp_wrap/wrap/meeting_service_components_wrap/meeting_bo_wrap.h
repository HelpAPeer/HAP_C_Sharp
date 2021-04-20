#pragma once
#include "../common_include.h"
BEGIN_ZOOM_SDK_NAMESPACE
class IMeetingServiceWrap;
IMeetingBOController* InitIMeetingBOControllerFunc(IMeetingBOControllerEvent* pEvent, IMeetingServiceWrap* pOwner);
void UninitIMeetingBOControllerFunc(IMeetingBOController* obj);
BEGIN_CLASS_DEFINE_WITHCALLBACK(IMeetingBOController, IMeetingBOControllerEvent)
NORMAL_CLASS(IMeetingBOController)
INIT_UNINIT_WITHEVENT_AND_OWNSERVICE(IMeetingBOController, IMeetingServiceWrap)

//	virtual bool SetEvent(IMeetingBOControllerEvent* event) = 0;
virtual SDKError SetEvent(IMeetingBOControllerEvent* pEvent)
{
	external_cb = pEvent;
	return SDKERR_SUCCESS;
}
//virtual IBOCreator*    GetBOCreatorHelper() = 0;
DEFINE_FUNC_0(GetBOCreatorHelper, IBOCreator*)

//virtual IBOData*	   GetBODataHelper() = 0;
DEFINE_FUNC_0(GetBODataHelper, IBOData*)


//virtual IBOAdmin* GetBOAdminHelper() = 0;
DEFINE_FUNC_0(GetBOAdminHelper, IBOAdmin*)

//virtual bool IsBOStarted() = 0;
DEFINE_FUNC_0(IsBOStarted, bool)

//virtual void OnNewBroadcastMessageReceived(const wchar_t* strMsg) = 0;
CallBack_FUNC_1(OnNewBroadcastMessageReceived, const wchar_t*, strMsg)


//	virtual void onHasCreatorRightsNotification(IBOCreator* pCreatorObj) = 0;
CallBack_FUNC_1(onHasCreatorRightsNotification, IBOCreator*, pCreatorObj)

// 	virtual void onHasAdminRightsNotification(IBOAdmin* pAdminObj) = 0;
CallBack_FUNC_1(onHasAdminRightsNotification, IBOAdmin*, pAdminObj)

//virtual void onHasAssistantRightsNotification(IBOAssistant* pAssistantObj) = 0;
CallBack_FUNC_1(onHasAssistantRightsNotification, IBOAssistant*, pAssistantObj)

//virtual void onHasAttendeeRightsNotification(IBOAttendee* pAttendeeObj) = 0;
CallBack_FUNC_1(onHasAttendeeRightsNotification, IBOAttendee*, pAttendeeObj)

//virtual void onHasDataHelperRightsNotification(IBOData* pDataHelperObj) = 0;
CallBack_FUNC_1(onHasDataHelperRightsNotification, IBOData*, pDataHelperObj)

//virtual void onLostCreatorRightsNotification() = 0;
CallBack_FUNC_0(onLostCreatorRightsNotification)

//	virtual void onLostAdminRightsNotification() = 0;
CallBack_FUNC_0(onLostAdminRightsNotification)

//	virtual void onLostAssistantRightsNotification() = 0;
CallBack_FUNC_0(onLostAssistantRightsNotification)

//	virtual void onLostAttendeeRightsNotification() = 0;
CallBack_FUNC_0(onLostAttendeeRightsNotification)

//	virtual void onLostDataHelperRightsNotification() = 0;
CallBack_FUNC_0(onLostDataHelperRightsNotification)
END_CLASS_DEFINE(IMeetingBOController)
END_ZOOM_SDK_NAMESPACE