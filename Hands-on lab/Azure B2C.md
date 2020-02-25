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
  - [Exercise 3: Enable Azure B2C for customer site](#exercise-3-enable-azure-b2c-for-customer-site)
    - [Task 1: Create a new directory](#task-1-create-a-new-directory)
    - [Task 2: Add a new application](#task-2-add-a-new-application)
    - [Task 3: Create Policies, Sign up and sign in](#task-3-create-policies-sign-up-and-sign-in)
    - [Task 4: Create a profile editing policy](#task-4-create-a-profile-editing-policy)
    - [Task 5: Modify the Contoso.App.SportsLeague.Web](#task-5-modify-the-contosoappsportsleagueweb)
    - [Task 6: Send authentication requests to Azure AD](#task-6-send-authentication-requests-to-azure-ad)
    - [Task 7: Display user information](#task-7-display-user-information)
    - [Task 8: Run the sample app](#task-8-run-the-sample-app)

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

## Exercise 3: Enable Azure B2C for customer site

Duration: 75 minutes

In this exercise, you will configure an Azure AD Business to Consumer (B2C) instance to enable authentication and policies for sign-in, sign-out and profile policies for the Contoso E-Commerce site.

### Task 1: Create a new directory

1. Log in to the Azure portal by using your existing Azure subscription or by starting a free trial. In the left-hand navigation menu, select **+Create a resource**. Then, search for and select **Azure Active Directory B2C** and select **Create** on the new blade that pops up.

    ![In the Everything blade, the active directory B2C text is in the Search field, and under Results, Azure Active Directory B2C displays.](../Media/Screenshots/image156.png "Everything blade")

2. In the new blade, select **Create a new Azure AD B2C Tenant**. Then, enter the name as **ContosoB2C** and a unique domain name and region. Then, select **Create**. After directory creation completes, select the link in the new information tile that reads **Click here to manage your new directory**.

    ![In the Azure Portal, under Create new B2C Tenant or Link to existing Tenant, Create a new Azure AD B2C Tenant is selected. On the right, the word \"here,\" which is a link, is circled in the Select here to manage your new directory message.](../Media/Screenshots/2019-03-28-09-29-30.png "Azure Portal")

    ![In the Azure Portal, under Create new B2C Tenant or Link to existing Tenant, Create a new Azure AD B2C Tenant is selected. On the right, the word \"here,\" which is a link, is circled in the Select here to manage your new directory message.](../Media/Screenshots/image157.png "Azure Portal")

3. Select the orange **No Subscription** message for instructions on how to link to an active subscription.

    ![In the Azure Portal, on the left, the \"No subscription linked to this B2C tenant or the Subscription needs your attention\" message is selected.](../Media/Screenshots/image158.png "Azure Portal")

    ![The three steps to link the B2C tenant to an Azure subscription are circled.](../Media/Screenshots/2019-08-25-17-45-26.png "Azure Portal")

    >**Note**: Essentially, you will need to switch back to your previous Azure AD tenant, and then launch the Azure AD B2C creation wizard again.

4. Select **Link an existing Azure AD B2C Tenant to my Azure subscription,** and select the Tenant you just created in the dropdown list and the existing resource group **contososports**. Then, select **Create**.

    ![In the Create new B2C Tenant or Link to existing Tenant blade, on the left, Link an existing Azure AD B2C Tenant to my Azure subscription is selected. On the right, in the Azure AD B2C Resource blade, the Azure AD B2C Tenant drop-down field is contosodb2ccustsitecp.onmicrosoft.com. The Resource group is using the existing contososports.](../Media/Screenshots/image159.png "Create new B2C Tenant or Link to existing Tenant")

5. After creation completes, open the new Azure AD B2C tenant by selecting **Resource Groups** in the navigation menu to the left and, then, **contososports**. Then, in the new blade, select the B2C tenant you just created.

    ![In the contososports resource group, the new B2C tenant is boxed in red.](../Media/Screenshots/b2ctenant_in_rg.png "Azure AD B2C Settings window")

6. In the new blade, select the **B2C Settings** tile for the new B2C tenant. You will be taken to the new subscription for this tenant.

    ![In the Azure AD B2C tenant window, on the left, All Settings is selected. In the bottom right section, the Azure AD B2C Settings tile is selected.](../Media/Screenshots/image160.png "Azure AD B2C Settings window")

7. In the new tab that opened, under the **MANAGE** menu area of the open **Azure AD B2C** blade, select **Applications**. Then, in the new pane, select **+Add**.

    ![In the Azure AD B2C Settings window, on the left, All Settings is selected. In the middle, under Settings, under Manage, Applications is selected. On the right, the Add button is selected.](../Media/Screenshots/b2c-add-app-link.png "Azure AD B2C Settings window")

### Task 2: Add a new application

1. Specify the following configuration options for the Web App:

   - Name: **Contoso B2C Application**

   - Include Web App / web API: **Yes**

   - Allow Implicit Flow: **Yes**

   - Reply URL: `https://[your web url].azurewebsites.net` _(This should be the HTTPS URL to the Contoso E-Commerce Site.)_

   ![The New application fields are set to the previously defined values.](../Media/Screenshots/image161.png "New application")

2. Select **Create**.

3. Back on the **Azure AD B2C** blade in the **Applications** screen, copy the application ID of your new application to Notepad to use later. Keep this tab open for the next task.

     ![B2C application name and ID values are shown.](../Media/Screenshots/2019-04-11-08-36-28.png "Azure AD B2C screen")

### Task 3: Create Policies, Sign up and sign in

1. Navigate back to the **Azure AD B2C** blade that was opened in the last task.

2. To enable sign-up on your application, you will need to create a sign-up policy. This policy describes the experiences consumers will go through during sign-up and the contents of tokens the application will receive on successful sign-ups. Select **User flows (policies)** link on the left menu and then **+New user flow** link at the top of the blade.

    ![In the Azure Portal, on the left, Azure AD B2C - User Flows selected.](../Media/Screenshots/2019-03-28-12-17-22.png "Azure AD B2C - User Flows selected")

3. Select the **Sign up and sign in** link.
  
    ![Recommended for most applications results is displayed. Sign up and sign in link is highlighted.](../Media/Screenshots/2019-03-28-12-20-42.png "Sign up and sign in link")

4. Enter **SignUp** in the **Name** field.

    ![The unique Azure AD B2C user flow name is displayed.](../Media/Screenshots/2019-04-11-08-40-58.png "User Flow Name")

5. Select **Identity providers**, and select **Email Signup**. Optionally, you can also select social identity providers (if previously configured for the tenant). Select **OK**.

    ![In the Add policy blade, Identity providers is selected. In the Select identity providers blade, Email signup is selected.](../Media/Screenshots/2019-03-28-12-25-35.png "Add policy and Select identity providers blades")

6. **Multifactor authentication** set to **Disabled**.

7. **User attributes and claims**.
    - Select the **Show more...** link

    ![In the Azure AD B2C - User flow policy - create user flow pane, the Show more link is highlighted after the default user attributes and claims.](../Media/Screenshots/2019-03-28-12-38-39.png "Show more link")

8. Select the following **Collect attributes**:

    - **Country/Region**
    - **Display Name**
    - **Postal Code**

9. Select the following **Return claims**:

    - **Display Name**
    - **Identity Provider**
    - **Postal Code**
    - **User is new**
    - **User's Object ID**
  
10. Review your selections, select **OK**.

    ![Azure AD B2C - User flow - Review the collection and return claims columns.](../Media/Screenshots/2019-03-28-12-44-04.png "Collection and return claims")

11. Select **Create**. Observe the policy just created appears as **B2C\_1\_SignUp** (the **B2C\_1\_** fragment is automatically added) in the **Sign-up policies** blade.

    >**Note**: The page may take a few minutes to load/refresh after you start creating the policy.

    ![Azure AD B2C - User flows list.  Shows the newly created flow.](../Media/Screenshots/2019-03-28-12-46-48.png "Azure AD B2C User Flow List")

12. Open the policy by selecting the link in the list e.g. **B2C\_1\_SignUp**.

13. Select **Run user flow** and open the dialog.
    ![In the Policies section, Sign-in policies is selected.](../Media/Screenshots/2019-03-28-12-52-27.png "Policies section")

14. **Run user flow** - Choose application and run user flow. 

    ![Choose application options are displayed. Contoso B2C Application option is selected. Run user flow button is displayed.](../Media/Screenshots/2019-03-28-12-55-51.png "Test the user flow")

15. A browser tab/window will open that looks like the following screenshot.

    ![Test the user flow.  Sample sign in presented in the browser.](../Media/Screenshots/2019-03-28-13-00-01.png "Test the user flow")

16. Select **Sign up now**.

   ![Sign up now fields are presented to the user](../Media/Screenshots/2019-03-28-13-02-25.png "Sign up now")

### Task 4: Create a profile editing policy

To enable profile editing on your application, you will need to create a profile editing policy. This policy describes the experiences that consumers will go through during profile editing and the contents of tokens that the application will receive on successful completion.

1. Select **User flows (polices)** link on the left blade.

2. Select **+ New user flow** link at the top of the blade.

3. Select the **All** tab link.

    ![The Create a user flow pane is displayed.  The ALL tab is selected. All user flows are displayed. The Profile editing has an arrow pointing at it.](../Media/Screenshots/2019-03-28-16-19-55.png "Select Profile Editing")

3. Select **Profile editing**.

4. The Name determines the profile editing policy name used by your application. For example, enter **EditProfile**.

    ![In the Add policy blade, Identity providers (1 Selected) is selected. Identities providers - select Local Account SignIn.](../Media/Screenshots/2019-03-28-16-24-26.png "select Local Account SignIn")

5. Select Identity providers, and then "**Local Account SignIn**."

6. Select the **Show more...** link

7. Select **Collect attributes**. Here, you choose attributes the consumer can view and edit.

    For now, select the following:

    - **Country/Region**
    - **Display Name**
    - **Job Title**
    - **Postal Code**
    - **State/Province**
    - **Street Address**

8. Select **Return claims**. Here, you choose claims you want returned in the tokens sent back to your application after a successful profile editing experience.

    For now, select the following:

    - **Display Name**
    - **Postal Code**

    ![Sign up - User attributes selected blade.](../Media/Screenshots/2019-03-28-16-28-53.png "Sign up - User attributes selected blade")

9. Select **OK**.

10. Select **Create**. Observe the policy just created appears as \"**B2C\_1\_EditProfile**\" (the **B2C\_1\_** fragment is automatically added) in the **Profile editing policies** blade.

11. Open the policy by selecting **B2C\_1\_EditProfile**, then **Run user flow**.

12. Select **Contoso B2C application** in the **Select Application** drop-down.

13. Select **Run user flow**. A new browser tab opens, and you can run through the profile editing consumer experience in your application.

### Task 5: Modify the Contoso.App.SportsLeague.Web

1. Expand the **Contoso.Apps.SportsLeague.Web** project. Find the **Startup.cs** code file, locate the `public void Configure(` method declaration, then add the following line of code to this method:

    ```csharp
    app.UseAuthorization();
    ```

    ![The Startup.cs file with the "app.UseAuthorization();" line of code highlighted.](../Media/Screenshots/2019-04-19-15-08-40.png "Startup.cs")

2. Locate the Azure AD B2C name by navigating to your resource group. Copy the name to Notepad.

    ![List of all of the resources within the ContosoSports resource group. Pointing to the B2C tenant name.](../Media/Screenshots/2019-03-28-16-51-14.png "Locate B2C tenant name")

3. Next, using the Azure Management Portal, using your main subscription, open the Contoso Web App blade, and select **Configuration**.

4. Add the following settings in the **Application Settings** section:

   - AzureADB2C:Instance - `https://login.microsoftonline.com/tfp/`.
   - AzureADB2C:ClientId - **B2C Application ID you copied down earlier**.
   - AzureADB2C:CallbackPath - `/signin-oidc-b2c`.
   - AzureADB2C:Domain - **[your Azure AD B2C name].onmicrosoft.com**.
   - AzureADB2C:SignUpSignInPolicyId - **B2C_1_SignUp**.
   - AzureADB2C:ResetPasswordPolicyId - **B2C_1_SSPR**.
   - AzureADB2C:EditProfilePolicyId - **B2C_1_EditProfile**.

5. Select **Save** when you are complete.

### Task 6: Send authentication requests to Azure AD

Your app is now properly configured to communicate with Azure AD B2C by using ASP.NET Core Identity. OWIN has taken care of all of the details of crafting authentication messages, validating tokens from Azure AD, and maintaining user session. All that remains is to initiate each user's flow.

1. Right select the **Controllers** folder, and select **Add** -\> **Controller**.

    ![In Solution Explorer, in the right-click menu for the Controllers folder, Add is selected, and from its menu, Controller is selected.](../Media/Screenshots/image177.png "Solution Explorer")

2. Select **MVC Controller -- Empty** and then select **Add**. Replace **DefaultController** value with **AccountController** in the **Add Controller** dialog box.

    ![On the left of the Add Scaffold window, Installed / Controller is selected. In the center of the window, MVC 5 Controller - Empty is selected.](../Media/Screenshots/image178.png "Add Scaffold window")

3. Add the following using statement to the top of the controller:

    ```csharp
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.Extensions.Configuration;
    using System.Threading.Tasks;
    ```

4. Locate the default controller **Index** method.

    ![The Default controller method Index is circled.](../Media/Screenshots/image179.png "Default controller method Index")

    Replace the method with the following code, then **Save** the file.

    ```csharp
    // Controllers\AccountController.cs

    public static string SignUpSignInPolicyId;
    public static string EditProfilePolicyId;

    public AccountController(IConfiguration configuration)
    {
        SignUpSignInPolicyId = configuration.GetValue<string>("AzureADB2C:SignUpSignInPolicyId");
        EditProfilePolicyId = configuration.GetValue<string>("AzureADB2C:EditProfilePolicyId");
    }

    public async Task SignIn()
    {
        if (!User.Identity.IsAuthenticated)
        {
            // To execute a policy, you simply need to trigger an OWIN challenge.
            // You can indicate which policy to use by specifying the policy id as the AuthenticationType
            await HttpContext.ChallengeAsync(SignUpSignInPolicyId,
                new AuthenticationProperties() { RedirectUri = "/" });
        }
    }

    public async Task SignUp()
    {
        if (!User.Identity.IsAuthenticated)
        {
            await HttpContext.ChallengeAsync(SignUpSignInPolicyId,
                new AuthenticationProperties() { RedirectUri = "/" });
        }
    }


    public async Task Profile()
    {
        if (User.Identity.IsAuthenticated)
        {
            await HttpContext.ChallengeAsync(EditProfilePolicyId,
                new AuthenticationProperties() { RedirectUri = "/" });
        }
    }

    public async Task SignOut()
    {
        if (User.Identity.IsAuthenticated)
        {
            await HttpContext.SignOutAsync();
        }
    }

    ```

### Task 7: Display user information

When you authenticate users by using OpenID Connect, Azure AD returns an ID token to the app that contains **claims**. These are assertions about the user. You can use claims to personalize your app. You can access user claims in your controllers via the ClaimsPrincipal.Current security principal object.

1. Open the **Controllers\\HomeController.cs** file and add the following using statements at the end of the other using statements at the top of the file.

    ```csharp
    using System.Linq;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Authorization
    ```

2. Still in the **Controllers\\HomeController.cs** file, add the following method to the **HomeController** class:

    ```csharp
    [Authorize]
    public ActionResult Claims()
    {
        Claim displayName = ClaimsPrincipal.Current.FindFirst(ClaimsPrincipal.Current.Identities.First().NameClaimType);
        ViewBag.DisplayName = displayName != null ? displayName.Value : string.Empty;
        return View();
    }
    ```

3. You can access any claim that your application receives in the same way. A list of all the claims the app receives is available for you on the **Claims** page. In Visual Studio on the Contoso.Apps.SportsLeague.Web object, right-click on **Views -\> Home,** select **Add -\> View** and name it **Claims.**  Select **OK**.

    ![In Solution Explorer, on the right-click menu for Views\\Home, Add is selected, and from its menu, View is selected.](../Media/Screenshots/image180.png "Solution Explorer")

4. Open the **Claims.cshtml** file and replace the code with the following:

    ```csharp
    @using System.Security.Claims
    @{
        ViewBag.Title = "Claims";
    }
    <h2>@ViewBag.Title</h2>

    <h4>Claims Present in the Claims Identity: @ViewBag.DisplayName</h4>

    <table class="table-hover claim-table">
        <tr>
            <th class="claim-type claim-data claim-head">Claim Type</th>
            <th class="claim-data claim-head">Claim Value</th>
        </tr>

        @foreach (Claim claim in ClaimsPrincipal.Current.Claims)
        {
            <tr>
                <td class="claim-type claim-data">@claim.Type</td>
                <td class="claim-data">@claim.Value</td>
            </tr>
        }
    </table>

    ```

5. Right-click on the **Views -\> Shared** folder, select **Add**, add a new **View**, and set it to **Create as a partial view**. Specify **\_LoginPartial** for the name.

    ![In Solution Explorer, on the right-click menu for Views\\Shared, Add is selected, and from its menu, View is selected.](../Media/Screenshots/image180.png  "Solution Explorer")

    !["Create as a partial view" is highlighted.](../Media/Screenshots/image180-b.png "Add MVC View")

6. Add the following code to the razor partial view to provide a sign-in and sign-out link as well as a link to edit the user's profile:

    ```html
    @if (User.Identity.IsAuthenticated)
    {
        <text>
            <ul class="nav navbar-nav navbar-right">
                <li>
                    <a id="profile-link">@User.Identity.Name</a>
                    <div id="profile-options" class="nav navbar-nav navbar-right">
                        <ul class="profile-links">
                            <li class="profile-link">
                                @Html.ActionLink("Edit Profile", "Profile", "Account")
                            </li>
                        </ul>
                    </div>
                </li>
                <li>
                    @Html.ActionLink("Sign out", "SignOut", "Account")
                </li>
            </ul>
        </text>
    }
    else
    {
        <ul class="nav navbar-nav navbar-right">
            <li>@Html.ActionLink("Sign up", "SignUp", "Account", routeValues: null, htmlAttributes: new { id = "signUpLink" })</li>
            <li>@Html.ActionLink("Sign in", "SignIn", "Account", routeValues: null, htmlAttributes: new { id = "loginLink" })</li>
        </ul>
    }

    ```

7. Open **Views\\Shared\\\_Layout.cshtml** in Visual Studio. Locate the header-top div. and add the line that starts with **@Html.ActionLink** and the line that starts with **@Html.Partial**.

    ```html
    <div class="header-top">
        <div class="container">
            <div class="row">
                <div class="header-top-left">
                <a href="#"><i class="fa fa-twitter"></i></a>
                <a href="#"><i class="fa fa-facebook"></i></a>
                <a href="#"><i class="fa fa-linkedin"></i></a>
                <a href="#"><i class="fa fa-instagram"></i></a>
                </div>
                <div class="header-top-right">
                    <a href="#" class="top-wrap"><span class="icon-phone">Call today: </span> (555) 555-8000</a>
                    @Html.ActionLink("Claims", "Claims", "Home")
                </div>
                @Html.Partial("_LoginPartial")
            </div>
        </div>
    </div>
    ```

### Task 8: Run the sample app

1. Right-click on the **Contoso.Apps.SportsLeague.Web** project, and select **Publish**. Follow the steps to deploy the updated application to the Microsoft Azure Web App.

    Launch a browser outside of Visual Studio for testing if the page loads in Visual Studio.

2. Test out Sign up.

3. Next, test Sign out.

4. When you select Claims and are not signed in, it will bring you to the sign-in page and then display the claim information. Sign in, and test Edit Profile.

    ![On the Contoso website, the following links are circled: Claims, Sign up, and Sign in.](../Media/Screenshots/image182.png "Contoso website")

    Claims information page![On the Contoso website, the following links are circled: Russell, Sign out, and Edit Profile.](../Media/Screenshots/image183.png "Contoso website, Claims information page")

![Kabel](../Media/Kabel.png "https://www.kabel.es")