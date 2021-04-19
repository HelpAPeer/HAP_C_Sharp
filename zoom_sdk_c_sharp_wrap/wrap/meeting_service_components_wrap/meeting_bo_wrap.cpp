#include "stdafx.h"
#include "meeting_bo_wrap.h"
#include "../meeting_service_wrap.h"

BEGIN_ZOOM_SDK_NAMESPACE

IMeetingBOController* InitIMeetingBOControllerFunc(IMeetingBOControllerEvent* pEvent, IMeetingServiceWrap* pOwner){

	if (pOwner && pOwner->GetSDKObj())
	{
		ZOOM_SDK_NAMESPACE::IMeetingBOController* pObj = pOwner->GetSDKObj()->GetMeetingBOController();
		if (pObj)
		{
			pObj->SetEvent(pEvent);
		}
		return pObj;
	}

	return NULL;
}

void UninitIMeetingBOControllerFunc(IMeetingBOController* obj)
{
	if (obj)
	{
		obj->SetEvent(NULL);
	}
}

IMPL_FUNC_0(IMeetingBOController, IsBOStarted, bool, false)
IMPL_FUNC_0(IMeetingBOController, GetBOCreatorHelper, IBOCreator*, NULL)
IMPL_FUNC_0(IMeetingBOController, GetBODataHelper, IBOData*, NULL)

END_ZOOM_SDK_NAMESPACE