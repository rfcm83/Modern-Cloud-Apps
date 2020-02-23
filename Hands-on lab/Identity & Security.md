![Microsoft Cloud Workshops](../Media/ms-cloud-workshop.png "Microsoft Cloud Workshops")

# Modern Cloud Apps

Before the hands-on lab setup guide

March 2020

## Table Of Contents
- [Modern cloud apps hands-on lab step-by-step](#modern-cloud-apps-hands-on-lab-step-by-step)
  - [Abstract and learning objectives](#abstract-and-learning-objectives)
  - [Overview](#overview)
  - [Solution architecture](#solution-architecture)
  - [Requirements](#requirements)
  - [Help references](#help-references)
  - [Exercise 2: Identity and Security](#exercise-2-identity-and-security)
    - [Task 1: Enable Azure AD Premium Trial](#task-1-enable-azure-ad-premium-trial)
    - [Task 2: Create a new Contoso user](#task-2-create-a-new-contoso-user)
    - [Task 3: Configure access control for the call center administration Web Application](#task-3-configure-access-control-for-the-call-center-administration-web-application)
      - [Subtask 1: Enable Azure AD Authentication](#subtask-1-enable-azure-ad-authentication)
      - [Subtask 2: Verify the call center administration website uses the access control logon](#subtask-2-verify-the-call-center-administration-website-uses-the-access-control-logon)
    - [Task 4: Apply custom branding for the Azure Active Directory logon page](#task-4-apply-custom-branding-for-the-azure-active-directory-logon-page)
    - [Task 5: Verify the branding has been successfully applied to the Azure Active Directory logon page](#task-5-verify-the-branding-has-been-successfully-applied-to-the-azure-active-directory-logon-page)

# Modern cloud apps hands-on lab step-by-step

## Abstract and learning objectives

In this hands-on lab, you will be challenged to implement an end-to-end scenario using a supplied sample that is based on Azure App Services, Microsoft Azure Functions, Azure SQL Database, Azure Logic Apps, and related services. The scenario will include implementing compute, storage, workflows, and monitoring, using various components of Microsoft Azure.

Please note that as opposed to the whiteboard design session, the lab is not focused on maintaining PCI compliance and using more advanced security features such as App Service Environment, Network Security Groups, and Application Gateway. The hands-on lab can be implemented on your own, but it is highly recommended to pair up with other members working on the lab to model a real-world experience and to allow each member to share their expertise for the overall solution.

By the end of this hands-on lab, you will have learned how to use several key services within Azure to improve overall functionality of the original solution, and to increase the security and scalability of the new and improved design.

## Overview

The Cloud Workshop: Modern Cloud Apps lab is a hands-on exercise that will challenge you to implement an end-to-end scenario using a supplied sample that is based on Microsoft Azure App Services and related services. The scenario will include implementing compute, storage, security, and scale using various components of Microsoft Azure. The lab can be implemented on your own, but it is highly recommended to pair up with additional team members to more closely model a real-world experience, and to allow members to share their expertise for the overall solution.

## Solution architecture

![A diagram that depicts the various Azure PaaS services for the solution. Azure AD Org is used for authentication to the call center app. Azure AD B2C for authentication is used for authentication to the client app. SQL Database for the backend customer data. Azure App Services for the web and API apps. Order processing includes using Functions, Logic Apps, Queues and Storage. Azure App Insights is used for telemetry capture.](../Media/Screenshots/image2.png "Solution Architecture Diagram")

## Requirements

1. Microsoft Azure subscription
2. Local machine or a virtual machine configured with Visual Studio 2019 Community Edition
3. Twilio account and/or personal cell phone to setup a trial Twilio account

## Help references

| Description | Links |
|:---------|:-------------|
| SQL firewall | <https://azure.microsoft.com/en-us/documentation/articles/sql-database-configure-firewall-settings/> |
| Deploying a Web App | <https://azure.microsoft.com/en-us/documentation/articles/web-sites-deploy/> |
| Deploying an API app | <https://azure.microsoft.com/en-us/documentation/articles/app-service-dotnet-deploy-api-app/> |
| Accessing an API app from a JavaScript client | <https://azure.microsoft.com/en-us/documentation/articles/app-service-api-javascript-client/> |
| SQL Database Geo-Replication overview | <https://azure.microsoft.com/en-us/documentation/articles/sql-database-geo-replication-overview/> |
| What is Azure AD? | <https://azure.microsoft.com/en-us/documentation/articles/active-directory-whatis/> |
| Azure Web Apps authentication | <http://azure.microsoft.com/blog/2014/11/13/azure-websites-authentication-authorization/> |
| View your access and usage reports | <https://msdn.microsoft.com/en-us/library/azure/dn283934.aspx> |
| Custom branding an Azure AD Tenant | <https://msdn.microsoft.com/en-us/library/azure/Dn532270.aspx> |
| Service Principal Authentication | <https://docs.microsoft.com/en-us/azure/app-service-api/app-service-api-dotnet-service-principal-auth> |
| Consumer Site B2C | <https://docs.microsoft.com/en-us/azure/active-directory-b2c/active-directory-b2c-devquickstarts-web-dotnet> |
| Getting Started with Active Directory B2C | <https://azure.microsoft.com/en-us/trial/get-started-active-directory-b2c/> |
| How to Delete an Azure Active Directory | <https://blog.nicholasrogoff.com/2017/01/20/how-to-delete-an-azure-active-directory-add-tenant/> |
| Run performance tests on your app | <http://blogs.msdn.com/b/visualstudioalm/archive/2015/09/15/announcing-public-preview-for-performance-load-testing-of-azure-webapp.aspx> |
| Application Insights Custom Events | <https://azure.microsoft.com/en-us/documentation/articles/app-insights-api-custom-events-metrics/> |
| Enabling Application Insights | <https://azure.microsoft.com/en-us/documentation/articles/app-insights-start-monitoring-app-health-usage/> |
| Detect failures | <https://azure.microsoft.com/en-us/documentation/articles/app-insights-asp-net-exceptions/> |
| Monitor performance problems | <https://azure.microsoft.com/en-us/documentation/articles/app-insights-web-monitor-performance/> |
| Creating a Logic App | <https://azure.microsoft.com/en-us/documentation/articles/app-service-logic-create-a-logic-app/> |
| Logic app connectors | <https://azure.microsoft.com/en-us/documentation/articles/app-service-logic-connectors-list/> |
| Logic Apps Docs | <https://docs.microsoft.com/en-us/azure/logic-apps/logic-apps-what-are-logic-apps> |
| Azure Functions -- create first function | <https://docs.microsoft.com/en-us/azure/azure-functions/functions-create-first-azure-function> |
| Azure Functions docs | <https://docs.microsoft.com/en-us/azure/logic-apps/logic-apps-azure-functions> |

## Exercise 2: Identity and Security

Duration: 75 Minutes

The Contoso call center admin application will only be accessible by users of the Contoso Active Directory environment. You have been asked to create a new Azure AD Tenant and secure the application so only users from the tenant can log on.

### Task 1: Enable Azure AD Premium Trial

>**Note**: This task is **optional**, and it is valid only if you are a global administrator on the Azure AD tenant associated with your subscription.

1. Navigate to the Azure Management portal, [http://portal.azure.com](http://portal.azure.com/), using a new tab or instance.

2. In the left-hand navigation menu, select **Azure Active Directory**.

    ![The Azure Active Directory menu option](../Media/Screenshots/image123.png "Azure Portal")

3. On the **Azure Active Directory** blade, locate and select the **Company branding** option.

    ![In the Azure Active Directory blade, Company branding is selected.](../Media/Screenshots/image124.png "Azure Active Directory blade")

4. In the right pane, select the **Get a free Premium trial...** link.

    ![On the left side of the Azure Active Directory blade, Company branding is selected. On the right side, the Get a free Premium trial link is selected.](../Media/Screenshots/image125.png "Azure Active Directory blade")

    If you already have a Premium Azure Active Directory, skip to Task 2.

5. On the **Activate** blade, select the **Free Trial** link within the **Azure AD Premium P2**, then select **Activate**.

    ![The Free trial link is selected on the Activate blade in the Azure AD Premium P2 box, and the Activate button is highlighted.](../Media/Screenshots/image126.png "Activate blade")

6. Close the **Azure Active Directory** blades.

### Task 2: Create a new Contoso user

>**Note**: This task is **optional**, and it is valid only if you are a global administrator on the Azure AD tenant associated with your subscription.

1. Navigate to the Azure Management portal, [http://portal.azure.com](http://portal.azure.com/), using a new tab or instance.

2. Select **Azure Active Directory** in the navigation menu to the left.

    ![Screenshot of Azure Active Directory menu option](../Media/Screenshots/image123.png "Azure Portal")

3. On the **Azure Active Directory** blade, select **Custom Domain names**.

    ![Custom Domain Names menu option screenshot.](../Media/Screenshots/image128.png "Custom Domain names")

4. Copy the **Domain Name** for your Azure AD Tenant. It will be in the format: *[your tenant\].onmicrosoft.com*.
    This will be used for creating the new user's Username.

    ![Under Name, the Domain Name is selected.](../Media/Screenshots/image129.png "Domain name")

5. On the **Azure Active Directory** blade, select **Users**.

    ![Under Manage, All users is selected.](../Media/Screenshots/image130.png "Azure Active Directory blade")

6. Select **+ New user** to add a new user.

    ![The + New User button is boxed in red on the Azure Active Directory blade.](../Media/Screenshots/image131.png "Azure Active Directory blade")

7. On the **User** blade, specify a user's **Name** and **User name**. Specify the **User name** to be at the domain name for your Azure AD Tenant. For example: *tbaker@\[your tenant\].onmicrosoft.com*.

    ![On the User blade, the two previously defined fields (Name and User name) are circled.](../Media/Screenshots/image132.png "User blade")

8. Select the **Show Password** checkbox, and make a note of the password for use later.

    ![The Show Password checkbox is selected.](../Media/Screenshots/image133.png "Password section")

9. Select **Create**.

    ![Screenshot of the Create button.](../Media/Screenshots/image134.png "Create button")

### Task 3: Configure access control for the call center administration Web Application

>**Note**: This task is **optional**, and it is valid only if you have the right to create applications in your Azure AD Tenant.

#### Subtask 1: Enable Azure AD Authentication

1. On the left navigation of the Azure Portal, select **App Services**.

    ![Screenshot of the App Services button.](../Media/Screenshots/image135.png "App Services button")

2. On the **App Services** blade, select the **Call Center Administration Web app**.

    ![In the App Services blade, under Name, contososportscallcentercp is selected.](../Media/Screenshots/image136.png "App Services blade")

3. Select the **Authentication / Authorization** tile.

    ![On the App Service blade, under Settings, Authentication / Authorization is selected.](../Media/Screenshots/image137.png "App Service blade")

4. Change **App Service Authentication** to **On**, and change the dropdown to **Log in with Azure Active Directory**.

    ![The Authentication / Authorization section displays with App Service Authentication button set to On, and Log in with Azure Active Directory selected in the Action to take when request is not authenticated drop-down list box.](../Media/Screenshots/image138.png "Authentication / Authorization section")

5. Select the **Azure Active Directory**.

    ![In the Authentication Providers section, Azure Active Directory (Not Configured) is selected.](../Media/Screenshots/image139.png "Authentication Providers section")

6. On the **Azure Active Directory Settings** blade, change **Management mode** to **Express**.

    ![At the bottom of the Azure Active Directory Settings blade, Management mode is set to Express.](../Media/Screenshots/image140.png "Azure Active Directory Settings blade")

7. Select **OK**.

    ![Screenshot of the OK button.](../Media/Screenshots/image141.png "OK button")

8. Change the **Action to take when request is not authenticated** option to **Login with Azure Active Directory**.

    ![The Action to take when request is not authenticated field is set to Log in with Azure Authentication.](../Media/Screenshots/image142.png "Action to take field")

9. In the **Authentication / Authorization** blade, select **Save**.

    ![The Save button is circled in the App Service blade.](../Media/Screenshots/image143.png "App Service blade")

#### Subtask 2: Verify the call center administration website uses the access control logon

1. Close your browser (or use an alternative), and launch a browser is **InPrivate or Incognito mode**. Navigate to the **Call Center Administration** website.

2. The browser will redirect to the non-branded Access Control logon URL. You can log on with your Microsoft account or the **Contoso test user** you created earlier.

    ![Microsoft login prompt.](../Media/Screenshots/image144.png "Microsoft login prompt")

3. After you log on and **accept the consent**, your browser will be redirected to the Contoso Sports League Admin webpage.

    ![The Contoso Sports League Admin webpage displays with one Completed Order.](../Media/Screenshots/image145.png "Contoso Sports League Admin webpage")

4. Verify in the upper-right corner you see the link **Logged In**. If it is not configured, you will see **Sign in**.

    ![Screenshot of the Logged In message.](../Media/Screenshots/image146.png "Logged in message") ![Screenshot of the Sign In link.](../Media/Screenshots/image147.png "Sign in link")

### Task 4: Apply custom branding for the Azure Active Directory logon page

>**Note**: this task is **optional**, and it is valid only if you are a global administrator on the Azure AD tenant associated with your subscription, and you completed the Enabling Azure AD Premium exercise.

1. Navigate to the Azure Management portal, [http://portal.azure.com](http://portal.azure.com/), using a new tab or instance.

2. In the navigation menu to the left, select **Azure Active Directory**.

    ![The Azure Active Directory menu option.](../Media/Screenshots/image123.png "Azure Active Directory")

3. On the **Azure Active Directory** blade, select **Company branding**.

    ![On the Azure Active Directory blade, Company branding is selected.](../Media/Screenshots/image148.png "Azure Active Directory blade")

4. select the **Configure...** information box.

    ![The Configure company branding link is selected.](../Media/Screenshots/image149.png "Configure company branding link")

5. On the **Configure company branding** blade, select the `default_signin_illustration.jpg` image file from `C:\MCW` for the **Sign-in page image**.

    ![The Configure company branding blade displays the default sign in picture of the Contoso sports league text, and a person on a bicycle. Below that, the Select a file field and browse icon is selected.](../Media/Screenshots/image150.png "Configure company branding blade")

6. Select the `logo-60-280.png` image file from the supplementary files for the **Banner image**.

    ![The Contoso sports league banner text displays.](../Media/Screenshots/image151.png "Contoso sports league banner")

7. Select **Save**.

    ![The Save button is circled on the Configure company branding blade.](../Media/Screenshots/image152.png "Configure company branding blade")

### Task 5: Verify the branding has been successfully applied to the Azure Active Directory logon page

1. Close any previously authenticated browser sessions to the call center administration website, reopen using InPrivate or Incognito mode, and navigate to the **call center administration** website.

2. The browser will redirect to the branded access control logon URL.

    ![The Call center administration Sign in webpage displays in an InPrivate / Incognito browser.](../Media/Screenshots/image153.png "Call center administration website")

3. After you log on, your browser will be redirected to the Contoso Sports League Admin webpage.

    ![The Contoso Sports League Admin webpage displays with one completed order.](../Media/Screenshots/image145.png "Contoso Sports League Admin webpage")

4. Verify in the upper-right corner you see the link **Logged in**.

    ![Screenshot of the Logged in message.](../Media/Screenshots/image146.png "Logged in message")

    >**Note**: If you run the app using localhost, ensure connection strings within all the appsettings.json files in the solution have the placeholders removed with actual values. Search on appsettings.json in the Visual Studio Solution Explorer to come up with the list.

![Kabel](../Media/Kabel.png "https://www.kabel.es")