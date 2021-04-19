#pragma once
using namespace System;
using namespace System::Windows;
#include "zoom_sdk_dotnet_wrap_def.h"
#include "h/meeting_service_components/meeting_breakout_rooms_interface_v2.h"
namespace ZOOM_SDK_DOTNET_WRAP
{
public interface class IBOCreatorDotNet
    {
    public:
        String ^ CreateBO(String ^ strBOName);
        bool UpdateBOName(String ^ strBOID, String ^ strNewBOName);
        bool RemoveBO(String ^ strBOID);
        bool AssignUserToBO(String ^ strUserID, String ^ strBOID);
        bool RemoveUserFromBO(String ^ strUserID, String ^ strBOID);
    };

public
    enum class BO_CTRL_USER_STATUS : int
    {
        BO_CTRL_USER_STATUS_UNASSIGNED = 1,
        BO_CTRL_USER_STATUS_ASSIGNED_NOT_JOIN = 2,
        BO_CTRL_USER_STATUS_IN_BO = 3,
        BO_CTRL_USER_STATUS_UNKNOWN = 4,
    };

public
    interface class IBOMeetingDotNet
    {
        String ^ GetBOID();
        String ^ GetBOName();
        array<String ^> ^ GetBOUserList();
    };

public
    interface class IBODataDotNet
    {
    public:
        array<String ^> ^ GetUnassginedUserList();
        array<String ^> ^ GetBOMeetingIDList();
        String ^ GetBOUserName(String ^ strUserID);
        BO_CTRL_USER_STATUS GetBOUserStatus(String ^ strUserID);
        bool IsBOUserMyself(String ^ strUserID);
        IBOMeetingDotNet ^ GetBOMeetingByID(String ^ strBOId);
    };

public
    interface class IBOAdminEventDotNet
    {
    public:
        void OnHelpRequestReceived(String ^ strUserID);
    };

public
    interface class IBOAdminDotNet
    {
    public:
        bool StartBO();
        bool StopBO();
        bool AssignNewUserToRunningBO(String ^ strUserID, String ^ strBOID);
        bool SwitchAssignedUserToRunningBO(String ^ strUserID, String ^ strBOID) = 0;
        bool CanStartBO();
        bool JoinBOByUserRequest(String ^ strUserID);
        bool IgnoreUserHelpRequest(String ^ strUserID);
        bool BroadcastMessage(String ^ strMsg);
    };

public
    interface class IBOAssistantDotNet
    {
    public:
    };

public
    interface class IBOAttendeeDotNet
    {
    public:
    };

public
    delegate void OnNewBroadcastMessageReceived(String ^ stBID);
public
    delegate void onHasCreatorRightsNotification(IBOCreatorDotNet ^ boc);
public
    delegate void onHasAdminRightsNotification(IBOAdminDotNet ^ boa);
public
    delegate void onHasAssistantRightsNotification(IBOAssistantDotNet ^ boa);
public
    delegate void onHasAttendeeRightsNotification(IBOAttendeeDotNet ^ boa);
public
    delegate void onHasDataHelperRightsNotification(IBODataDotNet ^ boa);

public
    delegate void onLostCreatorRightsNotification();
public
    delegate void onLostAdminRightsNotification();
public
    delegate void onLostAssistantRightsNotification();
public
    delegate void onLostAttendeeRightsNotification();
public
    delegate void onLostDataHelperRightsNotification();

public
    interface class IMeetingBOControllerDotNetWrap
    {
    public:
        void Add_CB_OnNewBroadcastMessageReceived(OnNewBroadcastMessageReceived ^ cb);
        void Remove_CB_OnNewBroadcastMessageReceived(OnNewBroadcastMessageReceived ^ cb);
        void Add_CB_onHasCreatorRightsNotification(onHasCreatorRightsNotification ^ cb);
        void Remove_CB_onHasCreatorRightsNotification(onHasCreatorRightsNotification ^ cb);
        void Add_CB_onHasAdminRightsNotification(onHasAdminRightsNotification ^ cb);
        void Remove_CB_onHasAdminRightsNotification(onHasAdminRightsNotification ^ cb);
        void Add_CB_onHasAssistantRightsNotification(onHasAssistantRightsNotification ^ cb);
        void Remove_CB_onHasAssistantRightsNotification(onHasAssistantRightsNotification ^ cb);
        void Add_CB_onHasAttendeeRightsNotification(onHasAttendeeRightsNotification ^ cb);
        void Remove_CB_onHasAttendeeRightsNotification(onHasAttendeeRightsNotification ^ cb);
        void Add_CB_onHasDataHelperRightsNotification(onHasDataHelperRightsNotification ^ cb);
        void Remove_CB_onHasDataHelperRightsNotification(onHasDataHelperRightsNotification ^ cb);

        void Add_CB_onLostCreatorRightsNotification(onLostCreatorRightsNotification ^ cb);
        void Remove_CB_onLostCreatorRightsNotification(onLostCreatorRightsNotification ^ cb);
        void Add_CB_onLostAdminRightsNotification(onLostAdminRightsNotification ^ cb);
        void Remove_CB_onLostAdminRightsNotification(onLostAdminRightsNotification ^ cb);
        void Add_CB_onLostAssistantRightsNotification(onLostAssistantRightsNotification ^ cb);
        void Remove_CB_onLostAssistantRightsNotification(onLostAssistantRightsNotification ^ cb);
        void Add_CB_onLostAttendeeRightsNotification(onLostAttendeeRightsNotification ^ cb);
        void Remove_CB_onLostAttendeeRightsNotification(onLostAttendeeRightsNotification ^ cb);
        void Add_CB_onLostDataHelperRightsNotification(onLostDataHelperRightsNotification ^ cb);
        void Remove_CB_onLostDataHelperRightsNotification(onLostDataHelperRightsNotification ^ cb);

        bool IsBOStarted();
        IBOCreatorDotNet ^ GetBOCreatorHelper();
        IBODataDotNet ^ GetBODataHelper();
    };

private
    ref class CMeetingBOControllerDotNetWrap sealed : public IMeetingBOControllerDotNetWrap
    {
        // TODO: Add your methods for this class here.
    public:
        static property CMeetingBOControllerDotNetWrap ^ Instance {
            CMeetingBOControllerDotNetWrap ^ get() { return m_Instance; }
        }

            void
            BindEvent();

        virtual bool IsBOStarted();
        virtual IBOCreatorDotNet ^ GetBOCreatorHelper();
        virtual IBODataDotNet ^ GetBODataHelper();

        virtual void Add_CB_OnNewBroadcastMessageReceived(OnNewBroadcastMessageReceived ^ cb)
        {
            event_OnNewBroadcastMessageReceived += cb;
        }

        virtual void Remove_CB_OnNewBroadcastMessageReceived(OnNewBroadcastMessageReceived ^ cb)
        {
            event_OnNewBroadcastMessageReceived -= cb;
        }

        virtual void Add_CB_onHasCreatorRightsNotification(onHasCreatorRightsNotification ^ cb)
        {
            event_onHasCreatorRightsNotification += cb;
        }

        virtual void Remove_CB_onHasCreatorRightsNotification(onHasCreatorRightsNotification ^ cb)
        {
            event_onHasCreatorRightsNotification -= cb;
        }

        virtual void Add_CB_onHasAdminRightsNotification(onHasAdminRightsNotification ^ cb)
        {
            event_onHasAdminRightsNotification += cb;
        }

        virtual void Remove_CB_onHasAdminRightsNotification(onHasAdminRightsNotification ^ cb)
        {
            event_onHasAdminRightsNotification -= cb;
        }

        virtual void Add_CB_onHasAssistantRightsNotification(onHasAssistantRightsNotification ^ cb)
        {
            event_onHasAssistantRightsNotification += cb;
        }

        virtual void Remove_CB_onHasAssistantRightsNotification(onHasAssistantRightsNotification ^ cb)
        {
            event_onHasAssistantRightsNotification -= cb;
        }

        virtual void Add_CB_onHasAttendeeRightsNotification(onHasAttendeeRightsNotification ^ cb)
        {
            event_onHasAttendeeRightsNotification += cb;
        }

        virtual void Remove_CB_onHasAttendeeRightsNotification(onHasAttendeeRightsNotification ^ cb)
        {
            event_onHasAttendeeRightsNotification -= cb;
        }

        virtual void Add_CB_onHasDataHelperRightsNotification(onHasDataHelperRightsNotification ^ cb)
        {
            event_onHasDataHelperRightsNotification += cb;
        }

        virtual void Remove_CB_onHasDataHelperRightsNotification(onHasDataHelperRightsNotification ^ cb)
        {
            event_onHasDataHelperRightsNotification -= cb;
        }

        virtual void Add_CB_onLostCreatorRightsNotification(onLostCreatorRightsNotification ^ cb)
        {
            event_onLostCreatorRightsNotification += cb;
        }
        virtual void Remove_CB_onLostCreatorRightsNotification(onLostCreatorRightsNotification ^ cb)
        {
            event_onLostCreatorRightsNotification -= cb;
        }
        virtual void Add_CB_onLostAdminRightsNotification(onLostAdminRightsNotification ^ cb)
        {
            event_onLostAdminRightsNotification += cb;
        }
        virtual void Remove_CB_onLostAdminRightsNotification(onLostAdminRightsNotification ^ cb)
        {
            event_onLostAdminRightsNotification -= cb;
        }
        virtual void Add_CB_onLostAssistantRightsNotification(onLostAssistantRightsNotification ^ cb)
        {
            event_onLostAssistantRightsNotification += cb;
        }
        virtual void Remove_CB_onLostAssistantRightsNotification(onLostAssistantRightsNotification ^ cb)
        {
            event_onLostAssistantRightsNotification -= cb;
        }
        virtual void Add_CB_onLostAttendeeRightsNotification(onLostAttendeeRightsNotification ^ cb)
        {
            event_onLostAttendeeRightsNotification += cb;
        }
        virtual void Remove_CB_onLostAttendeeRightsNotification(onLostAttendeeRightsNotification ^ cb)
        {
            event_onLostAttendeeRightsNotification -= cb;
        }
        virtual void Add_CB_onLostDataHelperRightsNotification(onLostDataHelperRightsNotification ^ cb)
        {
            event_onLostDataHelperRightsNotification += cb;
        }
        virtual void Remove_CB_onLostDataHelperRightsNotification(onLostDataHelperRightsNotification ^ cb)
        {
            event_onLostDataHelperRightsNotification -= cb;
        }

        void ProcBONewBroadcastMessageReceived(String ^ stBID);
        void ProcBOHasCreatorRightsNotification(IBOCreatorDotNet ^ boc);
        void ProcBOHasAdminRightsNotification(IBOAdminDotNet ^ boc);
        void ProcBOHasAssistantRightsNotification(IBOAssistantDotNet ^ boc);
        void ProcBOHasAttendeeRightsNotification(IBOAttendeeDotNet ^ boc);
        void ProcBOHasDataHelperRightsNotification(IBODataDotNet ^ boc);
        void ProcBOLostCreatorRightsNotification();
        void ProcBOLostAdminRightsNotification();
        void ProcBOLostAssistantRightsNotification();
        void ProcBOLostAttendeeRightsNotification();
        void ProcBOLostDataHelperRightsNotification();

    private:
        CMeetingBOControllerDotNetWrap(){};
        virtual ~CMeetingBOControllerDotNetWrap(){};
        event OnNewBroadcastMessageReceived ^ event_OnNewBroadcastMessageReceived;
        event onHasCreatorRightsNotification ^ event_onHasCreatorRightsNotification;
        event onHasAdminRightsNotification ^ event_onHasAdminRightsNotification;
        event onHasAssistantRightsNotification ^ event_onHasAssistantRightsNotification;
        event onHasAttendeeRightsNotification ^ event_onHasAttendeeRightsNotification;
        event onHasDataHelperRightsNotification ^ event_onHasDataHelperRightsNotification;

        event onLostCreatorRightsNotification ^ event_onLostCreatorRightsNotification;
        event onLostAdminRightsNotification ^ event_onLostAdminRightsNotification;
        event onLostAssistantRightsNotification ^ event_onLostAssistantRightsNotification;
        event onLostAttendeeRightsNotification ^ event_onLostAttendeeRightsNotification;
        event onLostDataHelperRightsNotification ^ event_onLostDataHelperRightsNotification;
        static CMeetingBOControllerDotNetWrap ^ m_Instance = gcnew CMeetingBOControllerDotNetWrap;
    };
}