#pragma once
#include "../common_include.h"
BEGIN_ZOOM_SDK_NAMESPACE
class IMeetingServiceWrap;
IMeetingBOController* InitIMeetingBOControllerFunc(IMeetingBOControllerEvent* pEvent, IMeetingServiceWrap* pOwner);
void UninitIMeetingBOControllerFunc(IMeetingBOController* obj);
BEGIN_CLASS_DEFINE_WITHCALLBACK(IMeetingBOController, IMeetingBOControllerEvent)
NORMAL_CLASS(IMeetingBOController)
INIT_UNINIT_WITHEVENT_AND_OWNSERVICE(IMeetingBOController, IMeetingServiceWrap)
virtual SDKError SetEvent(IMeetingBOControllerEvent* pEvent)
{
	external_cb = pEvent;
	return SDKERR_SUCCESS;
}
DEFINE_FUNC_0(GetBOCreatorHelper, IBOCreator*)
DEFINE_FUNC_0(GetBODataHelper, IBOData*)
DEFINE_FUNC_0(IsBOStarted, bool)

CallBack_FUNC_1(OnNewBroadcastMessageReceived, const wchar_t*, strMsg)
CallBack_FUNC_1(onHasCreatorRightsNotification, IBOCreator*, pCreatorObj)
CallBack_FUNC_1(onHasAdminRightsNotification, IBOAdmin*, pAdminObj)
CallBack_FUNC_1(onHasAssistantRightsNotification, IBOAssistant*, pAssistantObj)
CallBack_FUNC_1(onHasAttendeeRightsNotification, IBOAttendee*, pAttendeeObj)
CallBack_FUNC_1(onHasDataHelperRightsNotification, IBOData*, pDataHelperObj)
CallBack_FUNC_0(onLostCreatorRightsNotification)
CallBack_FUNC_0(onLostAdminRightsNotification)
CallBack_FUNC_0(onLostAssistantRightsNotification)
CallBack_FUNC_0(onLostAttendeeRightsNotification)
CallBack_FUNC_0(onLostDataHelperRightsNotification)
END_CLASS_DEFINE(IMeetingBOController)
END_ZOOM_SDK_NAMESPACE