#include "stdafx.h"
#include "meeting_bo_dotnet_wrap.h"
#include "zoom_sdk_dotnet_wrap_util.h"
#include "wrap/sdk_wrap.h"
namespace ZOOM_SDK_DOTNET_WRAP {

	//translate bo info
	private ref class IBOCreatorDotNetImpl sealed : public IBOCreatorDotNet
	{
	public:
		IBOCreatorDotNetImpl(ZOOM_SDK_NAMESPACE::IBOCreator* pInfo)
		{
			m_pInfo = pInfo;
		}
		virtual bool AssignUserToBO(String ^ strUserId, String^ strBOId)
		{
			if (m_pInfo)
				return m_pInfo->AssignUserToBO(PlatformString2WChar(strUserId), PlatformString2WChar(strBOId));
			return false;
		}
		virtual String^ CreateBO(String ^ strBOName)
		{
			if (m_pInfo)
				return WChar2PlatformString(m_pInfo->CreateBO(PlatformString2WChar(strBOName)));
			return nullptr;
		}
		virtual bool RemoveBO(String^ strBOId)
		{
			if (m_pInfo)
				return m_pInfo->RemoveBO(PlatformString2WChar(strBOId));
			return false;
		}
		virtual bool RemoveUserFromBO(String ^ strUserId, String^ strBOId)
		{
			if (m_pInfo)
				return m_pInfo->RemoveUserFromBO(PlatformString2WChar(strUserId), PlatformString2WChar(strBOId));
			return false;
		}
		virtual bool UpdateBOName(String^ strBOId, String ^ strNewBOId)
		{
			if (m_pInfo)
				return m_pInfo->UpdateBOName(PlatformString2WChar(strBOId), PlatformString2WChar(strNewBOId));
			return false;
		}

	private:
		IBOCreatorDotNetImpl() {}
		ZOOM_SDK_NAMESPACE::IBOCreator* m_pInfo;
	};

	IBOMeetingDotNet^ TranslateBOMeeting(ZOOM_SDK_NAMESPACE::IBOMeeting*);

	private ref class IBODataDotNetImpl sealed : public IBODataDotNet
	{
	public:
		IBODataDotNetImpl(ZOOM_SDK_NAMESPACE::IBOData* pInfo)
		{
			m_pInfo = pInfo;
		}
		virtual array<String^ >^  GetUnassginedUserList()
		{
			if (m_pInfo)
			{
				ZOOM_SDK_NAMESPACE::IList<const wchar_t*>* p = m_pInfo->GetUnassginedUserList();
				if (p)
				{
					int count = p->GetCount();
					array<String^ >^ p2 = gcnew array<String^ >(count);
					if (p2)
					{
						for (int i = 0; i < count; i++)
						{
							String^ userid = WChar2PlatformString(p->GetItem(i));
							p2[i] = userid;
						}
						return p2;
					}
				}
			}
			return nullptr;
		}
		virtual array<String^ >^ GetBOMeetingIDList()
		{
			if (m_pInfo)
			{
				ZOOM_SDK_NAMESPACE::IList<const wchar_t*>* p = m_pInfo->GetBOMeetingIDList();
				if (p)
				{
					int count = p->GetCount();
					array<String^ >^ p2 = gcnew array<String^ >(count);
					if (p2)
					{
						for (int i = 0; i < count; i++)
						{
							String^ userid = WChar2PlatformString(p->GetItem(i));
							p2[i] = userid;
						}
						return p2;
					}
				}
			}
			return nullptr;
		}
		virtual String^ GetBOUserName(String^ strUserID)
		{
			if (m_pInfo)
				return WChar2PlatformString(m_pInfo->GetBOUserName(PlatformString2WChar(strUserID)));
			return nullptr;
		}
		virtual BO_CTRL_USER_STATUS GetBOUserStatus(String ^ strUserID)
		{
			if (m_pInfo)
				return (BO_CTRL_USER_STATUS)m_pInfo->GetBOUserStatus(PlatformString2WChar(strUserID));
			return  BO_CTRL_USER_STATUS::BO_CTRL_USER_STATUS_UNKNOWN;
		}
		virtual bool IsBOUserMyself(String^ strUserID)
		{
			if (m_pInfo)
				return m_pInfo->IsBOUserMyself(PlatformString2WChar(strUserID));
			return false;
		}
		virtual IBOMeetingDotNet^ GetBOMeetingByID(String^ strBOId)
		{
			if (m_pInfo)
			{
				ZOOM_SDK_NAMESPACE::IBOMeeting*p = m_pInfo->GetBOMeetingByID(PlatformString2WChar(strBOId));
				return TranslateBOMeeting(p);
			}
			return nullptr;
		}

	private:
		IBODataDotNetImpl() {}
		ZOOM_SDK_NAMESPACE::IBOData* m_pInfo;
	};

	private ref class IBOMeetingDotNetImpl sealed : public IBOMeetingDotNet
	{
	public:
		IBOMeetingDotNetImpl(ZOOM_SDK_NAMESPACE::IBOMeeting* pInfo)
		{
			m_pInfo = pInfo;
		}
		virtual String^ GetBOID()
		{
			if (m_pInfo)
			{
				const wchar_t* p = m_pInfo->GetBOID();
				return WChar2PlatformString(p);
			}
			return nullptr;
		}
		virtual String^ GetBOName()
		{
			if (m_pInfo)
			{
				const wchar_t* p = m_pInfo->GetBOName();
				return WChar2PlatformString(p);
			}
			return nullptr;
		}
		virtual array<String^ >^ GetBOUserList()
		{
			if (m_pInfo)
			{
				ZOOM_SDK_NAMESPACE::IList<const wchar_t*>* p = m_pInfo->GetBOUserList();
				return Convert(p);
			}
			return nullptr;
		}

	private:
		IBOMeetingDotNetImpl() {}
		ZOOM_SDK_NAMESPACE::IBOMeeting* m_pInfo;
	};

	private ref class IBOAssistantDotNetImpl sealed : public IBOAssistantDotNet
	{
	public:
		IBOAssistantDotNetImpl(ZOOM_SDK_NAMESPACE::IBOAssistant* pInfo)
		{
			m_pInfo = pInfo;
		}
		ZOOM_SDK_NAMESPACE::IBOAssistant* m_pInfo;
	};

	private ref class IBOAttendeeDotNetImpl sealed : public IBOAttendeeDotNet
	{
	public:
		IBOAttendeeDotNetImpl(ZOOM_SDK_NAMESPACE::IBOAttendee* pInfo)
		{
			m_pInfo = pInfo;
		}
		ZOOM_SDK_NAMESPACE::IBOAttendee* m_pInfo;
	};

	private ref class IBOAdminDotNetImpl sealed : public IBOAdminDotNet
	{
	public:
		IBOAdminDotNetImpl(ZOOM_SDK_NAMESPACE::IBOAdmin* pInfo)
		{
			m_pInfo = pInfo;
		}

		virtual bool StartBO()
		{
			if (m_pInfo)
				return m_pInfo->StartBO();
			return false;
		}

		virtual bool StopBO()
		{
			if (m_pInfo)
				return m_pInfo->StopBO();
			return false;
		}

		virtual bool AssignNewUserToRunningBO(String^ strUserID, String^ strBOID)
		{
			if (m_pInfo)
				return m_pInfo->AssignNewUserToRunningBO(PlatformString2WChar(strUserID), PlatformString2WChar(strBOID));
			return false;
		}

		virtual bool SwitchAssignedUserToRunningBO(String^ strUserID, String^ strBOID)
		{
			if (m_pInfo)
				return m_pInfo->SwitchAssignedUserToRunningBO(PlatformString2WChar(strUserID), PlatformString2WChar(strBOID));
			return false;
		}

		virtual bool CanStartBO()
		{
			if (m_pInfo)
				return m_pInfo->CanStartBO();
			return false;
		}

		virtual bool JoinBOByUserRequest(String^ strUserID)
		{
			if (m_pInfo)
				return m_pInfo->JoinBOByUserRequest(PlatformString2WChar(strUserID));
			return false;
		}

		virtual bool IgnoreUserHelpRequest(String^ strUserID)
		{
			if (m_pInfo)
				return m_pInfo->IgnoreUserHelpRequest(PlatformString2WChar(strUserID));
			return false;
		}

		virtual bool BroadcastMessage(String^ strUserID)
		{
			if (m_pInfo)
				return m_pInfo->BroadcastMessage(PlatformString2WChar(strUserID));
			return false;
		}

	private:
		IBOAdminDotNetImpl() {}
		ZOOM_SDK_NAMESPACE::IBOAdmin* m_pInfo;
	};

	IBOCreatorDotNetImpl^ TranslateBOCreator(ZOOM_SDK_NAMESPACE::IBOCreator* p)
	{
		if (p)
		{
			return gcnew IBOCreatorDotNetImpl(p);
		}
		return nullptr;
	}

	IBODataDotNetImpl^ TranslateBOData(ZOOM_SDK_NAMESPACE::IBOData* p)
	{
		if (p)
		{
			return gcnew IBODataDotNetImpl(p);
		}
		return nullptr;
	}

	IBOMeetingDotNet^ TranslateBOMeeting(ZOOM_SDK_NAMESPACE::IBOMeeting* p)
	{
		if (p)
		{
			return gcnew IBOMeetingDotNetImpl(p);
		}
		return nullptr;
	}

	IBOAdminDotNet^ TranslateBOAdmin(ZOOM_SDK_NAMESPACE::IBOAdmin* p)
	{
		if (p)
		{
			return gcnew IBOAdminDotNetImpl(p);
		}
		return nullptr;
	}

	IBOAssistantDotNet^ TranslateBOAssistant(ZOOM_SDK_NAMESPACE::IBOAssistant* p)
	{
		if (p)
		{
			return gcnew IBOAssistantDotNetImpl(p);
		}
		return nullptr;
	}

	IBOAttendeeDotNet^ TranslateBOAttendee(ZOOM_SDK_NAMESPACE::IBOAttendee* p)
	{
		if (p)
		{
			return gcnew IBOAttendeeDotNetImpl(p);
		}
		return nullptr;
	}

	//
	//translate event
	class MeetingBOControllerEventHandler
	{
	public:
		static MeetingBOControllerEventHandler& GetInst()
		{
			static MeetingBOControllerEventHandler inst;
			return inst;
		}

		void OnNewBroadcastMessageReceived(const wchar_t* stBID)
		{
			if (CMeetingBOControllerDotNetWrap::Instance)
				CMeetingBOControllerDotNetWrap::Instance->ProcBONewBroadcastMessageReceived(WChar2PlatformString(stBID));
		}

		void onHasCreatorRightsNotification(ZOOM_SDK_NAMESPACE::IBOCreator * boc)
		{
			if (CMeetingBOControllerDotNetWrap::Instance)
				CMeetingBOControllerDotNetWrap::Instance->ProcBOHasCreatorRightsNotification(TranslateBOCreator(boc));
		}

		void OnHasAdminRightsNotification(ZOOM_SDK_NAMESPACE::IBOAdmin* boc)
		{
			if (CMeetingBOControllerDotNetWrap::Instance)
				CMeetingBOControllerDotNetWrap::Instance->ProcBOHasAdminRightsNotification(TranslateBOAdmin(boc));
		}

		void OnHasAssistantRightsNotification(ZOOM_SDK_NAMESPACE::IBOAssistant* boc)
		{
			if (CMeetingBOControllerDotNetWrap::Instance)
				CMeetingBOControllerDotNetWrap::Instance->ProcBOHasAssistantRightsNotification(TranslateBOAssistant(boc));
		}

		void OnHasAttendeeRightsNotification(ZOOM_SDK_NAMESPACE::IBOAttendee* boc)
		{
			if (CMeetingBOControllerDotNetWrap::Instance)
				CMeetingBOControllerDotNetWrap::Instance->ProcBOHasAttendeeRightsNotification(TranslateBOAttendee(boc));
		}

		void OnHasDataHelperRightsNotification(ZOOM_SDK_NAMESPACE::IBOData* boc)
		{
			if (CMeetingBOControllerDotNetWrap::Instance)
				CMeetingBOControllerDotNetWrap::Instance->ProcBOHasDataHelperRightsNotification(TranslateBOData(boc));
		}

		void OnLostCreatorRightsNotification()
		{
			if (CMeetingBOControllerDotNetWrap::Instance)
				CMeetingBOControllerDotNetWrap::Instance->ProcBOLostCreatorRightsNotification();
		}

		void OnLostAdminRightsNotification()
		{
			if (CMeetingBOControllerDotNetWrap::Instance)
				CMeetingBOControllerDotNetWrap::Instance->ProcBOLostAdminRightsNotification();
		}

		void OnLostAssistantRightsNotification()
		{
			if (CMeetingBOControllerDotNetWrap::Instance)
				CMeetingBOControllerDotNetWrap::Instance->ProcBOLostAssistantRightsNotification();
		}

		void OnLostAttendeeRightsNotification()
		{
			if (CMeetingBOControllerDotNetWrap::Instance)
				CMeetingBOControllerDotNetWrap::Instance->ProcBOLostAttendeeRightsNotification();
		}

		void OnLostDataHelperRightsNotification()
		{
			if (CMeetingBOControllerDotNetWrap::Instance)
				CMeetingBOControllerDotNetWrap::Instance->ProcBOLostDataHelperRightsNotification();
		}
	private:
		MeetingBOControllerEventHandler() {};
	};
	//

	void CMeetingBOControllerDotNetWrap::BindEvent()
	{
		ZOOM_SDK_NAMESPACE::IMeetingBOControllerWrap& meetingBO = ZOOM_SDK_NAMESPACE::CSDKWrap::GetInst().GetMeetingServiceWrap().GetMeetingBOController();
		meetingBO.m_cbOnNewBroadcastMessageReceived =
			std::bind(&MeetingBOControllerEventHandler::OnNewBroadcastMessageReceived,
				&MeetingBOControllerEventHandler::GetInst(), std::placeholders::_1);
		meetingBO.m_cbonHasCreatorRightsNotification =
			std::bind(&MeetingBOControllerEventHandler::onHasCreatorRightsNotification,
				&MeetingBOControllerEventHandler::GetInst(), std::placeholders::_1);
		meetingBO.m_cbonHasAdminRightsNotification =
			std::bind(&MeetingBOControllerEventHandler::OnHasAdminRightsNotification,
				&MeetingBOControllerEventHandler::GetInst(), std::placeholders::_1);
		meetingBO.m_cbonHasAssistantRightsNotification =
			std::bind(&MeetingBOControllerEventHandler::OnHasAssistantRightsNotification,
				&MeetingBOControllerEventHandler::GetInst(), std::placeholders::_1);
		meetingBO.m_cbonHasAttendeeRightsNotification =
			std::bind(&MeetingBOControllerEventHandler::OnHasAttendeeRightsNotification,
				&MeetingBOControllerEventHandler::GetInst(), std::placeholders::_1);
		meetingBO.m_cbonHasDataHelperRightsNotification =
			std::bind(&MeetingBOControllerEventHandler::OnHasDataHelperRightsNotification,
				&MeetingBOControllerEventHandler::GetInst(), std::placeholders::_1);

		meetingBO.m_cbonLostCreatorRightsNotification =
			std::bind(&MeetingBOControllerEventHandler::OnLostCreatorRightsNotification,
				&MeetingBOControllerEventHandler::GetInst());
		meetingBO.m_cbonLostAdminRightsNotification =
			std::bind(&MeetingBOControllerEventHandler::OnLostAdminRightsNotification,
				&MeetingBOControllerEventHandler::GetInst());
		meetingBO.m_cbonLostAssistantRightsNotification =
			std::bind(&MeetingBOControllerEventHandler::OnLostAssistantRightsNotification,
				&MeetingBOControllerEventHandler::GetInst());
		meetingBO.m_cbonLostAttendeeRightsNotification =
			std::bind(&MeetingBOControllerEventHandler::OnLostAttendeeRightsNotification,
				&MeetingBOControllerEventHandler::GetInst());
		meetingBO.m_cbonLostDataHelperRightsNotification =
			std::bind(&MeetingBOControllerEventHandler::OnLostDataHelperRightsNotification,
				&MeetingBOControllerEventHandler::GetInst());
	}

	void CMeetingBOControllerDotNetWrap::ProcBONewBroadcastMessageReceived(String^ stBID)
	{
		event_OnNewBroadcastMessageReceived(stBID);
	}

	void CMeetingBOControllerDotNetWrap::ProcBOHasCreatorRightsNotification(IBOCreatorDotNet^ boc)
	{
		event_onHasCreatorRightsNotification(boc);
	}

	void CMeetingBOControllerDotNetWrap::ProcBOHasAdminRightsNotification(IBOAdminDotNet^ boc)
	{
		event_onHasAdminRightsNotification(boc);
	}

	void CMeetingBOControllerDotNetWrap::ProcBOHasAssistantRightsNotification(IBOAssistantDotNet^ boc)
	{
		event_onHasAssistantRightsNotification(boc);
	}

	void CMeetingBOControllerDotNetWrap::ProcBOHasAttendeeRightsNotification(IBOAttendeeDotNet^ boc)
	{
		event_onHasAttendeeRightsNotification(boc);
	}

	void CMeetingBOControllerDotNetWrap::ProcBOHasDataHelperRightsNotification(IBODataDotNet^ boc)
	{
		event_onHasDataHelperRightsNotification(boc);
	}

	void CMeetingBOControllerDotNetWrap::ProcBOLostCreatorRightsNotification()
	{
		event_onLostCreatorRightsNotification();
	}

	void CMeetingBOControllerDotNetWrap::ProcBOLostAdminRightsNotification()
	{
		event_onLostAdminRightsNotification();
	}

	void CMeetingBOControllerDotNetWrap::ProcBOLostAssistantRightsNotification()
	{
		event_onLostAssistantRightsNotification();
	}

	void CMeetingBOControllerDotNetWrap::ProcBOLostAttendeeRightsNotification()
	{
		event_onLostAttendeeRightsNotification();
	}

	void CMeetingBOControllerDotNetWrap::ProcBOLostDataHelperRightsNotification()
	{
		event_onLostDataHelperRightsNotification();
	}

	bool CMeetingBOControllerDotNetWrap::IsBOStarted()
	{
		return ZOOM_SDK_NAMESPACE::CSDKWrap::GetInst().GetMeetingServiceWrap().
			GetMeetingBOController().IsBOStarted();
	}

	IBOCreatorDotNet^ CMeetingBOControllerDotNetWrap::GetBOCreatorHelper()
	{
		return TranslateBOCreator(ZOOM_SDK_NAMESPACE::CSDKWrap::GetInst().GetMeetingServiceWrap().
			GetMeetingBOController().GetBOCreatorHelper());
	}

	IBODataDotNet^ CMeetingBOControllerDotNetWrap::GetBODataHelper()
	{
		return TranslateBOData(ZOOM_SDK_NAMESPACE::CSDKWrap::GetInst().GetMeetingServiceWrap().
			GetMeetingBOController().GetBODataHelper());
	}
}