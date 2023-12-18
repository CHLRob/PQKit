using System;
using System.Runtime.InteropServices;


namespace PQKitPlugin
{
    /// <summary>
    /// PQKit plugin class
    /// </summary>
    /// 
    public class CPQKitPlugin
    {
        public IPQPlatformComponent LoadPQKit()
        {
            Guid g = new Guid("32087F6B-AABF-420F-AFEB-5B1EAF4D88F2");
            Type t = Type.GetTypeFromCLSID(g);
            if (t == null)
            {
                return null;
            }
            object resObj = null;
            try
            {
                resObj = Activator.CreateInstance(t);
            }
            catch (Exception ex)
            {
                return null;
            }
            return (IPQPlatformComponent)resObj;
        }
    }

    public struct struct_PQKitOption
    {
        public int nEmbedded;
    }

    #region PQKit callback interface
    [Guid("0637B701-1636-49B6-9DF3-06FD0474D930")]
    [TypeLibType(4288)]
    public interface IPQPlatformComponentCallBack
    {
        [DispId(1)]
        void Fire_Initialize_Result(int lResult);
        [DispId(2)]
        void Fire_RunCMD_Result(int lResult);
        [DispId(3)]
        void Fire_GetData_Result(int lResult);
        [DispId(4)]
        void Notify_Raise_Dockwindow(int i_nType);
        [DispId(5)]
        void Fire_Login_Result(int i_nLoginType);
        [DispId(6)]
        void Fire_Path_Generate_Result(int i_bSuccess, int i_nPathCount, int i_nIndex, uint i_ulPathID);
        [DispId(7)]
        void Fire_Element_Pickup(uint i_ulObjID, int i_lEntityID);
        [DispId(8)]
        void Fire_RButton_Up(int i_lPosX,int i_lPosY);
        [DispId(9)]
        void Fire_LButton_Up(int i_lPosX, int i_lPosY);
        [DispId(10)]
        void Fire_Menu_Pop(uint i_ulObjID, int i_lPosX, int i_lPosY,out int o_nHandled);
    }
    #endregion

    #region PQKit interface
    [ComConversionLoss]
    [Guid("25686FB1-DA0D-4B16-969B-D9613837CB4D")]
    [TypeLibType(4288)]
    public interface IPQPlatformComponent
    {
        [DispId(1)]
        void pq_InitPlatformComponent(IPQPlatformComponentCallBack pCallBack, int hParentHwnd, string bsName, string bsPWD);
        [DispId(2)]
        void pq_GetPlatformView(out int hView);
        [DispId(3)]
        void pq_RunCommand(string bsCommandID, ulong wParam, long lParam, string bsParam, object varParam, out long lResult);
        [DispId(4)]
        void pq_GetAllDataObjectsByType(int i_lObjType, out string o_sObjNames, out string o_sObjIDs);
        [DispId(5)]
        void Doc_get_obj_joint_count(uint i_ulObjID, out int o_nCount);
        [DispId(6)]
        IntPtr Doc_get_obj_joints(uint i_ulObjID, out int i_nJointsCount);
        [DispId(7)]
        IntPtr Doc_get_obj_links(uint i_ulObjID, out int o_nLinksCount);
        [DispId(8)]
        void Doc_get_obj_velocity(uint i_ulObjID, out double o_dVelocity, out double o_dRAD);
        [DispId(9)]
        IntPtr Tool_get_tcp_posture(uint i_ulObjID, string i_chTcpName, int i_nPostureType, out int o_nPostureCount);
        [DispId(10)]
        IntPtr Robot_get_end_posture(uint i_ulObjID, int i_nPostureType, out int o_nPostureArraySize);
        [DispId(11)]
        void Doc_get_obj_name(uint i_ulObjID, out string o_bsName);
        [DispId(12)]
        IntPtr Doc_get_obj_posture(uint i_ulObjID, int i_nPostureType, out int o_nPostureArraySize);
        [DispId(13)]
        void Doc_set_obj_color(uint i_ulObjID, double i_dR, double i_dG, double i_dB);
        [DispId(14)]
        void pq_CloseDocument(string i_bsDocName);
        [DispId(15)]
        [return: ComAliasName("RPCLib.ULONG_PTR")]
        ulong pq_GetModelTreeView();
        [DispId(16)]
        [return: ComAliasName("RPCLib.ULONG_PTR")]
        ulong pq_GetDebugTreeView();
        [DispId(17)]
        [return: ComAliasName("RPCLib.ULONG_PTR")]
        ulong pq_GetOutPutView();
        [DispId(18)]
        [return: ComAliasName("RPCLib.ULONG_PTR")]
        ulong pq_GetRobotControlView();
        [DispId(19)]
        void PQAPIImportPointsToPath(uint i_ulRobotID, ref double i_dPosition, ref int i_nInstruct, ref double i_dVelocity, ref double i_dSpeedPercent, ref int i_nApproach, int i_nPointCount, string i_PathName, string i_GroupName, uint i_uCoordinateID, out uint o_PathID, int i_bToolEndPosture = 0);
        [DispId(20)]
        void PQAPITransPosition(ref double i_dInputPosition, int i_nTargetType, ushort i_usCount, out double o_dTargetPosition);
        [DispId(21)]
        void PQAPIDeletePathGroup(uint i_ulRobotID, string bsName);
        [DispId(22)]
        void PQAPIGetPointsID(uint i_uPathID, out object o_varIDArray);
        [DispId(23)]
        void PQAPIGetPointInfo(uint i_uPointID, int i_nPostureCount, out double o_dPointPosture, out double o_dVelocity, out double o_dSpeedPercent, out int o_nInstruct, out int o_nApproach, int i_nPosType);
        [DispId(24)]
        void PQAPIAddCustomEvent(ref uint i_uPointsID, int i_nPointCount, uint i_uExecuteObjID, int i_nEventPosition, string i_bsEventName, string i_bsContent);
        [DispId(25)]
        void PQAPIPutViewBGC(double i_dFromR, double i_dFromG, double i_dFromB, double i_dToR, double i_dToG, double i_dToB);
        [DispId(26)]
        void PQAPIGetObjPositionAttitudeType(uint i_ulObjID, out int o_nType);
        [DispId(27)]
        void PQAPISetPathInverType(uint i_uPathID, int i_nType);
        [DispId(28)]
        void PQAPISetActiveEngine(uint i_uEngineID);
        [DispId(29)]
        void PQAPIGetActiveEngine(out uint o_uEngineID);
        [DispId(30)]
        void PQAPIGetRobotJointsFromPoints(uint i_ulPointID, out object o_varJointsArray);
        [DispId(31)]
        void PQAPICreateExternalLink(uint i_ulEngineID, uint i_ulGuideID, int i_nAngle, int i_bSyncPosition, uint i_ulPositionerID);
        [DispId(32)]
        void PQAPIDeleteExternalLink(uint i_ulEngineID, uint i_ulExternalID, int i_bClearAll);
        [DispId(33)]
        void PQAPIGetExternalJointsFromPoints(uint i_ulPointID, uint i_uExternalID, out object o_varJointsArray);
        [DispId(34)]
        void PQAPIGetPathFromRobot(uint i_ulRobotID, out string o_sNames, out string o_sIDs);
        [DispId(35)]
        void PQAPIGetPathStatus(uint i_ulPathID, out int o_nStatus);
        [DispId(36)]
        void PQAPIGetPointCustomEventInfo(uint i_uID, out int o_bHasCustomEvent, out string o_sName, out string o_sContent, out string o_sPosition);
        [DispId(37)]
        void PQAPIAddTransitPath(uint i_uPathAID, uint i_uPathBID, out uint o_PathID);
        [DispId(38)]
        void PQAPIAddAbsJointPath(uint i_ulRobotID, ref double i_dRobotJoints, ushort i_usRCount, ref double i_dGuideJoints, ushort i_usGCount, ref double i_dPositionerJoints, ushort i_usPCount, ref double i_dVelocity, ref double i_dSpeedPercent, ref int i_nApproach, int i_nPointCount, uint i_uPathID);
        [DispId(39)]
        void PQAPIDeletePathGroupAll(uint i_ulRobotID);
        [DispId(40)]
        void PQAPICreatePathCoordinateRelation(uint i_ulPathID, uint i_ulCoordinateID);
        [DispId(41)]
        void PQAPIModifyExternalAxleBatch(ref uint i_ulPathIDs, int i_nPathCount, uint i_ulExternalID, ref double i_dExternalJoints, int i_nJointsCount);
        [DispId(42)]
        void PQIKCalInverseKinematics6R(uint i_ulRobotID, ref double i_EndPosture, int i_nEndPostureCount, ref double io_pJointValues, int i_nJointValuesCount, int i_nAxis1Cfg, int i_nAxis2Cfg, int i_nAxis4Cfg, int i_nAxis6Cfg, out int o_pPtStatus);
        [DispId(43)]
        void PQAPICreateBoxPart(double i_dLength, double i_dWidth, double i_dHeight, double i_dR, double i_dG, double i_dB, string i_PartName, out uint o_PartID);
        [DispId(44)]
        void PQAPICreateCatchEvent(uint i_uPartID, uint i_uPointID, int i_bPrePoint, int i_bCatch, string i_EventName);
        [DispId(45)]
        void PQAPIModifyPointPosture(uint i_ulPointID, ref double i_dPosture, int i_nPostureArraySize, int i_nPostureType = 1);
        [DispId(46)]
        void PQAPICalibratePart(uint i_ulPartID, ref double i_dSrcPosition, ref double i_dDesPosition, uint i_uCoordinateID);
        [DispId(47)]
        void PQAPIGetWorkPartVertexCount(uint i_ulPartID, out int o_nCount);
        [DispId(48)]
        void PQAPIGetWorkPartVertex(uint i_ulPartID, uint i_uCoordinateID, int i_nCount, out double i_dSrcPosition);
        [DispId(49)]
        void PQAPIModifyPathGroupIndex(uint i_ulRobotID, string bsSrcName, string bsTarName, int i_bBefore);
        [DispId(50)]
        void PQAPIInverseKinematics(uint i_ulRobotID, ref double i_EndPosture, int i_nEndPostureCount, int i_nPostureType, ref double io_pJointValues, int i_nJointValuesCount, ref int i_nAxisCfg, int i_nAxisCfgCount, out int o_pPtStatus, int i_bToolEndPosture = 0);
        [DispId(51)]
        void PQAPIInverseKinematicsFanuc(uint i_ulRobotID, ref double i_EndPosture, int i_nEndPostureCount, ref double io_pJointValues, int i_nJointValuesCount, ref int i_nAxisCfg, int i_nAxisCfgCount, out int o_pPtStatus);
        [DispId(52)]
        void PQAPISetAxisConfig6R(uint i_uPathID, ref int i_nAxisCfg, int i_nAxisCfgCount);
        [DispId(53)]
        void Robot_get_base_coordinate(uint i_ulRobotID, out uint o_uCoordinateID);
        [DispId(54)]
        void Path_Set_Post_Coordinate(uint i_ulPathID, uint i_ulCoorID);
        [DispId(55)]
        void Path_change_order(uint i_ulSrcPathID, uint i_ulTarPathID, int i_bAhead);
        [DispId(56)]
        void Path_feature_set_round(uint i_ulPathID, int i_nTime);
        [DispId(57)]
        void Path_modify_external_axis(uint i_ulPathID, ref uint i_ulGuideID, int i_nGuideCount, ref double i_dGuideData, int i_nGuideDataCount);
        [DispId(58)]
        void Path_add_entry_point(uint i_ulPathID, double i_dInOffset, double i_dOutOffset, int i_nInInstruction, int i_nOutInstruction);
        [DispId(59)]
        void Path_insert_from_point(uint i_ulRobotID, int i_nPtCount, ref double i_dPosition, int i_nPosType, ref int i_nInstruct, ref double i_dVelocity, ref double i_dSpeedPercent, ref int i_nApproach, string i_PathName, string i_GroupName, uint i_uCoordinateID, int i_bToolEndPosture, out uint o_PathID);
        [DispId(60)]
        void Path_get_group_path(uint i_ulRobotID, string i_GroupName, out object o_sNames, out object o_sIDs);
        [DispId(61)]
        void Path_get_point_count(uint i_ulPathID, out int o_nCount);
        [DispId(62)]
        void Doc_get_pathgroup_name(uint i_ulRobotID, out object o_varNameArray);
        [DispId(63)]
        void Doc_get_obj_bytype(int i_lType, out object o_sNames, out object o_sIDs);
        [DispId(64)]
        void Doc_set_obj_visibility(uint i_ulObjID, int i_bShow);
        [DispId(65)]
        void Math_trans_posture_to_rotationmatrix(ref double i_dPosture, int i_nType, out double o_dTranslation);
        [DispId(66)]
        void Path_get_pointgroup_info(uint i_ulPathID, out int o_nPtGroupCount, out object o_arrGroupPtCount);
        [DispId(67)]
        void Doc_collide_obj_single(uint i_ulObjAID, uint i_ulObjBID, out int o_bCollide);
        [DispId(68)]
        void Doc_collide_obj_multiple(ref uint i_ulObjAIDs, int i_nACount, ref uint i_ulObjBIDs, int i_nBCount, out object o_collideObjs);
        [DispId(69)]
        void Doc_set_obj_joints(uint i_ulObjID, ref double i_dJoints, int i_nJointArraySize);
        [DispId(70)]
        void Doc_set_obj_links(uint i_ulObjID, ref double i_dLinks, int i_nLinkArraySize);
        [DispId(71)]
        void Doc_set_obj_velocity(uint i_ulObjID, double i_dVelocity, double i_dRAD);
        [DispId(72)]
        void Tool_set_tcp_posture(uint i_ulObjID, string i_chTcpName, ref double i_dTcpPosture, int i_nPostureArraySize, int i_nPostureType);
        [DispId(73)]
        void Doc_set_obj_name(uint i_ulObjID, string i_bsName);
        [DispId(74)]
        void Doc_set_obj_posture(uint i_ulObjID, ref double i_dPosture, int i_nPostureArraySize, int i_nPostureType);
        [DispId(75)]
        void Doc_get_name(out string o_sName);
        [DispId(76)]
        void Point_modify_poture_batch(uint i_ulPathID, int i_nStartIndex, int i_nEndIndex, ref double i_dPosture, int i_nPostureArraySize, int i_nPostureType);
        [DispId(77)]
        void PQAPISetOption(ref struct_PQKitOption i_stuOption);
        [DispId(78)]
        void Path_get_generate_face(uint i_ulPathID, out int o_nFaceCount, [ComAliasName("RPCLib.LONG_PTR")] IntPtr o_lpFacePtr);
        [DispId(79)]
        void Path_insert_from_joint(uint i_ulRobotID, ref double i_dRobotJoints, int i_nRobotJointsSize, ref double i_dGuideJoints, int i_nGuideJointsSize, ref double i_dPositionerJoints, int i_nPositionerJointsSize, int i_nPointCount, ref int i_nInstruct, ref double i_dVelocity, ref double i_dSpeedPercent, ref int i_nApproach, string i_PathName, string i_GroupName, uint i_uCoordinateID, out uint o_PathID);
        [DispId(80)]
        void Path_get_generate_shape(uint i_ulPathID, out int o_nShapeCount, [ComAliasName("RPCLib.LONG_PTR")] IntPtr o_lpShapePtr);
        [DispId(81)]
        void PQAPIFree([ComAliasName("RPCLib.LONG_PTR")] long i_ptrData);
        [DispId(82)]
        void PQAPIFreeArray([ComAliasName("RPCLib.LONG_PTR")] long i_ptrDataArray);
        [DispId(83)]
        void Robot_get_tool(uint i_ulRobotID, out uint o_ulToolID);
        [DispId(84)]
        IntPtr Tool_get_tcp(uint i_ulID, out int o_nTcpCount, out string o_chTcpNames);
        [DispId(85)]
        void Path_set_tcp(uint i_ulID, string i_chTcpName);
        [DispId(86)]
        void Path_get_tcp(uint i_ulID, out string o_chTcpName);
        [DispId(87)]
        void Path_get_post_coordinate(uint i_ulPathID, out uint o_ulCoorID);
        [DispId(88)]
        void Path_get_relation_coordinate(uint i_ulPathID, out uint o_ulCoorID);
        [DispId(89)]
        void Doc_get_coordinate_posture(uint i_ulCoorID, int i_nPostureType, ref double i_dPosture);
        [DispId(90)]
        void Doc_add_sim_collide_data(ref uint i_vCollisionCheckIDS, int i_nCheckIDSCount, ref uint i_vCollisionWithIDS, int i_nWithIDSCount);
        [DispId(91)]
        void Doc_clear_sim_collide_data();
        [DispId(92)]
        void Doc_set_dockwindow_visible(int i_nDockType, int i_bVisible);
        [DispId(93)]
        void pq_CloseComponent();
        [DispId(94)]
        void Point_get_posture_batch(uint i_ulPathID, int i_nStartIndex, int i_nCount, int i_nPostureType, uint i_uCoordinateID, out int o_nPostureArraySize,ref IntPtr o_dPosture);
        [DispId(95)]
        void Doc_obj_rotate_local_X(uint i_ulObjID, double i_dRAD);
        [DispId(96)]
        void Doc_obj_rotate_local_Y(uint i_ulObjID, double i_dRAD);
        [DispId(97)]
        void Doc_obj_rotate_local_Z(uint i_ulObjID, double i_dRAD);
        [DispId(98)]
        void Doc_create_coordinate(uint i_ulBaseCoordinateID, int i_nPostureType, ref double i_dPosture, int i_nPostureSize, string i_chName);




    }
    #endregion

}