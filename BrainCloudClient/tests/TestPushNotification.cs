using NUnit.Core;
using NUnit.Framework;
using BrainCloud;
using BrainCloud.Common;
using System.Collections.Generic;
using JsonFx.Json;

namespace BrainCloudTests
{
    [TestFixture]
    public class TestPushNotification : TestFixtureBase
    {
        [Test]
        public void DeregisterAllPushNotificationDeviceTokens()
        {
            TestResult tr = new TestResult();

            BrainCloudClient.Instance.PushNotificationService.DeregisterAllPushNotificationDeviceTokens(
                tr.ApiSuccess, tr.ApiError);

            tr.Run();
        }

        [Test]
        public void DeregisterPushNotificationDeviceToken()
        {
            TestResult tr = new TestResult();

            BrainCloudClient.Instance.PushNotificationService.RegisterPushNotificationDeviceToken(
                Platform.iOS,
                "GARBAGE_TOKEN",
                tr.ApiSuccess, tr.ApiError);

            tr.Run();

            tr.Reset();
            BrainCloudClient.Instance.PushNotificationService.DeregisterPushNotificationDeviceToken(
                Platform.iOS,
                "GARBAGE_TOKEN",
                tr.ApiSuccess, tr.ApiError);

            tr.Run();
        }

        [Test]
        public void RegisterPushNotificationDeviceToken()
        {
            TestResult tr = new TestResult();

            BrainCloudClient.Instance.PushNotificationService.RegisterPushNotificationDeviceToken(
                Platform.iOS,
                "GARBAGE_TOKEN",
                tr.ApiSuccess, tr.ApiError);

            tr.Run();
        }

        [Test]
        public void SendSimplePushNotification()
        {
            TestResult tr = new TestResult();

            BrainCloudClient.Instance.PushNotificationService.SendSimplePushNotification(
                GetUser(Users.UserA).ProfileId,
                "Test message",
                tr.ApiSuccess, tr.ApiError);

            tr.Run();
        }

        [Test]
        public void SendRichPushNotification()
        {
            TestResult tr = new TestResult();

            BrainCloudClient.Instance.PushNotificationService.SendRichPushNotification(
                GetUser(Users.UserA).ProfileId,
                1,
                tr.ApiSuccess, tr.ApiError);

            tr.Run();
        }

        [Test]
        public void SendRichPushNotificationWithParams()
        {
            TestResult tr = new TestResult();

            BrainCloudClient.Instance.PushNotificationService.SendRichPushNotificationWithParams(
                GetUser(Users.UserA).ProfileId,
                1,
                Helpers.CreateJsonPair("1", GetUser(Users.UserA).Id),
                tr.ApiSuccess, tr.ApiError);

            tr.Run();
        }

        [Test]
        public void TestSendTemplatedPushNotificationToGroup()
        {
            TestResult tr = new TestResult();

            BrainCloudClient.Instance.GroupService.CreateGroup("testLBGroup", "test", null, null, null, null, null, tr.ApiSuccess, tr.ApiError);
            tr.Run();

            var data = tr.m_response["data"] as Dictionary<string, object>;
            var groupId = (string)data["groupId"];

            BrainCloudClient.Instance.PushNotificationService.SendTemplatedPushNotificationToGroup(
                groupId,
                1,
                Helpers.CreateJsonPair("1", GetUser(Users.UserA).Id),
                tr.ApiSuccess, tr.ApiError);
            tr.Run();

            BrainCloudClient.Instance.GroupService.DeleteGroup(groupId, -1, tr.ApiSuccess, tr.ApiError);
            tr.Run();
        }

        [Test]
        public void TestSendNormalizedPushNotificationToGroup()
        {
            TestResult tr = new TestResult();

            BrainCloudClient.Instance.GroupService.CreateGroup("testLBGroup", "test", null, null, null, null, null, tr.ApiSuccess, tr.ApiError);
            tr.Run();

            var data = tr.m_response["data"] as Dictionary<string, object>;
            var groupId = (string)data["groupId"];

            BrainCloudClient.Instance.PushNotificationService.SendNormalizedPushNotificationToGroup(
                groupId,
                "{ \"body\": \"content of message\", \"title\": \"message title\" }",
                Helpers.CreateJsonPair("1", GetUser(Users.UserA).Id),
                tr.ApiSuccess, tr.ApiError);
            tr.Run();

            BrainCloudClient.Instance.GroupService.DeleteGroup(groupId, -1, tr.ApiSuccess, tr.ApiError);
            tr.Run();
        }

        [Test]
        public void TestScheduleNormalizedPushNotificationUTC() 
        {
            TestResult tr = new TestResult();
            
            BrainCloudClient.Instance.PushNotificationService.ScheduleNormalizedPushNotificationUTC(
                 GetUser(Users.UserA).ProfileId,
                "{ \"body\": \"content of message\", \"title\": \"message title\" }",
                Helpers.CreateJsonPair("1", GetUser(Users.UserA).Id),
                42,
                tr.ApiSuccess, tr.ApiError);
            
            tr.Run();
        }

        [Test]
        public void TestScheduleNormalizedPushNotificationMinutes() 
        {

            TestResult tr = new TestResult();
            
            BrainCloudClient.Instance.PushNotificationService.ScheduleNormalizedPushNotificationUTC(
                GetUser(Users.UserA).ProfileId,
                "{ \"body\": \"content of message\", \"title\": \"message title\" }",
                Helpers.CreateJsonPair("1", GetUser(Users.UserA).Id),
                0,
                tr.ApiSuccess, tr.ApiError);

            tr.Run();
        }

        [Test]
        public void TestScheduleRichPushNotificationUTC() 
        {
            TestResult tr = new TestResult();

            BrainCloudClient.Instance.PushNotificationService.ScheduleRichPushNotificationUTC(
                GetUser(Users.UserA).ProfileId,
                1,
                Helpers.CreateJsonPair("1", GetUser(Users.UserA).Id),
                0,
                tr.ApiSuccess, tr.ApiError);

            tr.Run();
        }

        [Test]
        public void TestScheduleRichPushNotificationMinutes()
        {
            TestResult tr = new TestResult();
            
            BrainCloudClient.Instance.PushNotificationService.ScheduleRichPushNotificationMinutes(
                GetUser(Users.UserA).ProfileId,
                1,
                Helpers.CreateJsonPair("1", GetUser(Users.UserA).Id),
                0,
                tr.ApiSuccess, tr.ApiError);

            tr.Run();
        }

        [Test]
        public void TestSendNormalizedPushNotification()
        {
            TestResult tr = new TestResult();

            BrainCloudClient.Instance.PushNotificationService.SendNormalizedPushNotification(
                GetUser(Users.UserA).ProfileId,
                "{ \"body\": \"content of message\", \"title\": \"message title\" }",
                Helpers.CreateJsonPair("1", GetUser(Users.UserA).Id),
                tr.ApiSuccess, tr.ApiError);
            tr.Run();
        }

        [Test]
        public void TestSendNormalizedPushNotificationBatch()
        {
            TestResult tr = new TestResult();

            BrainCloudClient.Instance.PushNotificationService.SendNormalizedPushNotificationBatch(
                new[] { GetUser(Users.UserA).ProfileId, GetUser(Users.UserB).ProfileId },
                "{ \"body\": \"content of message\", \"title\": \"message title\" }",
                Helpers.CreateJsonPair("1", GetUser(Users.UserA).Id),
                tr.ApiSuccess, tr.ApiError);
            tr.Run();
        }
        

        [Test]
        public void TestScheduleRawPushNotificationUTC()
        {
            TestResult tr = new TestResult();

            string fcmContent = "{ \"notification\": { \"body\": \"content of message\", \"title\": \"message title\" }, \"data\": { \"customfield1\": \"customValue1\", \"customfield2\": \"customValue2\" }, \"priority\": \"normal\" }";
            string iosContent = "{ \"aps\": { \"alert\": { \"body\": \"content of message\", \"title\": \"message title\" }, \"badge\": 0, \"sound\": \"gggg\" } }";
            string facebookContent = "{\"template\": \"content of message\"}";

            BrainCloudClient.Instance.PushNotificationService.ScheduleRawPushNotificationUTC(
                GetUser(Users.UserA).ProfileId,
                fcmContent,
                iosContent,
                facebookContent,
                42,
                tr.ApiSuccess, tr.ApiError);

            tr.Run();
        }

        [Test]
        public void TestScheduleRawPushNotificationMinutes()
        {
            TestResult tr = new TestResult();

            string fcmContent = "{ \"notification\": { \"body\": \"content of message\", \"title\": \"message title\" }, \"data\": { \"customfield1\": \"customValue1\", \"customfield2\": \"customValue2\" }, \"priority\": \"normal\" }";
            string iosContent = "{ \"aps\": { \"alert\": { \"body\": \"content of message\", \"title\": \"message title\" }, \"badge\": 0, \"sound\": \"gggg\" } }";
            string facebookContent = "{\"template\": \"content of message\"}";

            BrainCloudClient.Instance.PushNotificationService.ScheduleRawPushNotificationMinutes(
                GetUser(Users.UserA).ProfileId,
                fcmContent,
                iosContent,
                facebookContent,
                1,
                tr.ApiSuccess, tr.ApiError);

            tr.Run();
        }
        
        [Test]
        public void TestSendRawPushNotification()
        {
            TestResult tr = new TestResult();

            string fcmContent = "{ \"notification\": { \"body\": \"content of message\", \"title\": \"message title\" }, \"data\": { \"customfield1\": \"customValue1\", \"customfield2\": \"customValue2\" }, \"priority\": \"normal\" }";
            string iosContent = "{ \"aps\": { \"alert\": { \"body\": \"content of message\", \"title\": \"message title\" }, \"badge\": 0, \"sound\": \"gggg\" } }";
            string facebookContent = "{\"template\": \"content of message\"}";

            BrainCloudClient.Instance.PushNotificationService.SendRawPushNotification(
                 GetUser(Users.UserA).ProfileId,
                 fcmContent,
                 iosContent,
                 facebookContent,
                 tr.ApiSuccess, tr.ApiError);

            tr.Run();
        }

        [Test]
        public void TestSendRawPushNotificationBatch()
        {
            TestResult tr = new TestResult();

            string fcmContent = "{ \"notification\": { \"body\": \"content of message\", \"title\": \"message title\" }, \"data\": { \"customfield1\": \"customValue1\", \"customfield2\": \"customValue2\" }, \"priority\": \"normal\" }";
            string iosContent = "{ \"aps\": { \"alert\": { \"body\": \"content of message\", \"title\": \"message title\" }, \"badge\": 0, \"sound\": \"gggg\" } }";
            string facebookContent = "{\"template\": \"content of message\"}";

            BrainCloudClient.Instance.PushNotificationService.SendRawPushNotificationBatch(
                 new[] { GetUser(Users.UserA).ProfileId, GetUser(Users.UserB).ProfileId },
                 fcmContent,
                 iosContent,
                 facebookContent,
                 tr.ApiSuccess, tr.ApiError);

            tr.Run();
        }

        [Test]
        public void TestSendRawPushNotificationToGroup()
        {
            TestResult tr = new TestResult();

            BrainCloudClient.Instance.GroupService.CreateGroup("testLBGroup", "test", null, null, null, null, null, tr.ApiSuccess, tr.ApiError);
            tr.Run();

            var data = tr.m_response["data"] as Dictionary<string, object>;
            var groupId = (string)data["groupId"];

            string fcmContent = "{ \"notification\": { \"body\": \"content of message\", \"title\": \"message title\" }, \"data\": { \"customfield1\": \"customValue1\", \"customfield2\": \"customValue2\" }, \"priority\": \"normal\" }";
            string iosContent = "{ \"aps\": { \"alert\": { \"body\": \"content of message\", \"title\": \"message title\" }, \"badge\": 0, \"sound\": \"gggg\" } }";
            string facebookContent = "{\"template\": \"content of message\"}";

            BrainCloudClient.Instance.PushNotificationService.SendRawPushNotificationToGroup(
                 groupId,
                 fcmContent,
                 iosContent,
                 facebookContent,
                 tr.ApiSuccess, tr.ApiError);

            BrainCloudClient.Instance.GroupService.DeleteGroup(groupId, -1, tr.ApiSuccess, tr.ApiError);

            tr.Run();
        }
        
    }
}