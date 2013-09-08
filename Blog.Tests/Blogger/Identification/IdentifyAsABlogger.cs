using System;
using System.Linq;
using StoryQ;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Blog.Tests;
using Blog.Domain.Commands;
using Blog.ReadModel.Repository;
using System.Security.Cryptography;
using System.Text;
using Blog.Domain.Errors;
using System.Collections.Generic;

[TestClass]
public class IdentifyAsBlogger_
{
	Guid createdUser = Guid.NewGuid();
	Guid sameUser = Guid.NewGuid();
	Guid changePwdUser = Guid.NewGuid();

	[TestMethod]
	public void IdentifyAsBlogger()
	{
		new Story("Identify as Blogger")
			 .InOrderTo("restrict what can be done on the site")
			 .AsA("Blogger")
			 .IWant("to identify myself")

						.WithScenario("Creating User")
							 .Given(Nothing)
							 .When(AUserIsCreated, createdUser)
							 .Then(ThatUserExists)

						.WithScenario("Creating User With No Username")
							 .Given(Nothing)
							 .When(AUserIsCreatedWithNoUsername)
							 .Then(AUserMustHaveAUsernameErrorIsRaised)

						.WithScenario("Creating User With The Same Username")
							 .Given(AUserIsCreated, sameUser)
							 .When(AUserIsCreatedWithTheSameUsername, sameUser)
							 .Then(AUserAlreadyExistsErrorIsRaised)

						.WithScenario("Login")
							 .Given(AnExistingUser)
							 .When(LoggedInWithTheCorrectUsernameAndPassword)
							 .Then(FunctionsThatOnlyABloggerCanDoCanBePerformed)

						.WithScenario("Bad Login")
							 .Given(Nothing)
							 .When(LoggedInWithTheIncorrectUsernameOrPassword)
							 .Then(AWrongUsernameOrPasswordErrorIsRaised)

						.WithScenario("Not Logged In")
							 .Given(BloggerIsNotLoggedIn)
							 .When(AnyCommandIsProcessedForTheBlogger)
							 .Then(ANotLoggedInErrorShouldBeRaised)

						.WithScenario("Change Password")
							 .Given(AUserIsCreated, changePwdUser)
							 .When(TheyChangeTheirPassword)
							 .Then(TheyCanLoginWithTheNewPassword)
							 .And(TheyCantLoginWithTheOldPassword)
			 .Execute();
	}

	private void AUserIsCreated(Guid id)
	{
		_.Receive(new CreateUser() { Id = id, Username = id.ToString(), Salt = id, Password = id.ToString() });
	}

	private void ThatUserExists()
	{
		Assert.IsNotNull(Repositories.Users.Get(createdUser));
	}

	Exception noUsername;
	private void AUserIsCreatedWithNoUsername()
	{
		try
		{
			_.Receive(new CreateUser() { Id = createdUser, Salt = createdUser, Password = createdUser.ToString() });
		}
		catch (Exception ex)
		{
			noUsername = ex;
		}
	}

	private void AUserMustHaveAUsernameErrorIsRaised()
	{
		Assert.IsInstanceOfType(noUsername, typeof(UserMustHaveAUsernameError));
	}

	Exception sameUsername;
	private void AUserIsCreatedWithTheSameUsername(Guid id)
	{
		try
		{
			_.Receive(new CreateUser() { Id = id, Username = id.ToString(), Salt = id, Password = id.ToString() });
		}
		catch (Exception ex)
		{
			sameUsername = ex;
		}
	}

	private void AUserAlreadyExistsErrorIsRaised()
	{
		Assert.IsInstanceOfType(sameUsername, typeof(UserAlreadyExistsError));
	}

	private void Nothing()
	{
	}

	private void AnExistingUser()
	{
		Assert.IsNotNull(Repositories.Users.Get(createdUser));
	}

	private void LoggedInWithTheCorrectUsernameAndPassword()
	{
		_.Receive(new Login() { Username = createdUser.ToString(), Password = createdUser.ToString() });
	}

	private void FunctionsThatOnlyABloggerCanDoCanBePerformed()
	{
		var id = Guid.NewGuid();
		var sessionId = Repositories.Sessions.Get().Where(s => s.UserId == createdUser).Select(s => s.Id).First();
		_.Receive(new CreatePost() { Id = id, Title = "Title", WhenCreated = DateTime.Now, SessionId = sessionId });
		_.Receive(new EditPost() { Id = id, Content = "Content", WhenEdited = DateTime.Now, SessionId = sessionId });
		_.Receive(new PublishPost() { Id = id, WhenPublished = DateTime.Now, SessionId = sessionId });
	}

	Exception ex1;
	Exception ex2;
	private void LoggedInWithTheIncorrectUsernameOrPassword()
	{
		try
		{
			_.Receive(new Login() { Username = Guid.NewGuid().ToString(), Password = "doesn't matter" });
		}
		catch (Exception ex)
		{
			ex1 = ex;
		}
		try
		{
			_.Receive(new Login() { Username = createdUser.ToString(), Password = "doesn't matter" });
		}
		catch (Exception ex)
		{
			ex2 = ex;
		}
	}

	private void AWrongUsernameOrPasswordErrorIsRaised()
	{
		Assert.IsInstanceOfType(ex1, typeof(WrongUsernameOrPasswordError));
		Assert.IsInstanceOfType(ex2, typeof(WrongUsernameOrPasswordError));
	}

	private void BloggerIsNotLoggedIn()
	{
	}

	List<Exception> exNotLoggedIn = new List<Exception>();
	private void AnyCommandIsProcessedForTheBlogger()
	{
		try
		{
			_.Receive(new CreatePost() { Id = Guid.NewGuid(), Title = "Title", WhenCreated = DateTime.Now, SessionId = Guid.NewGuid() });
		}
		catch (Exception ex)
		{
			exNotLoggedIn.Add(ex);
		}
		try
		{
			_.Receive(new EditPost() { Id = Guid.NewGuid(), Content = "Content", WhenEdited = DateTime.Now, SessionId = Guid.NewGuid() });
		}
		catch (Exception ex)
		{
			exNotLoggedIn.Add(ex);
		}
		try
		{
			_.Receive(new PublishPost() { Id = Guid.NewGuid(), WhenPublished = DateTime.Now, SessionId = Guid.NewGuid() });
		}
		catch (Exception ex)
		{
			exNotLoggedIn.Add(ex);
		}
	}

	private void ANotLoggedInErrorShouldBeRaised()
	{
		Assert.AreEqual(3, exNotLoggedIn.Count);
		Assert.IsTrue(exNotLoggedIn.All(e => e is NotLoggedInError));
	}

	private void TheyChangeTheirPassword()
	{
		_.Receive(new ChangePassword() { Id = changePwdUser, OldPassword = changePwdUser.ToString(), NewPassword = new string(changePwdUser.ToString().Reverse().ToArray()) });
	}

	private void TheyCanLoginWithTheNewPassword()
	{
		_.Receive(new Login() { Username = changePwdUser.ToString(), Password = new string(changePwdUser.ToString().Reverse().ToArray()) });
	}

	private void TheyCantLoginWithTheOldPassword()
	{
		try
		{
			_.Receive(new Login() { Username = changePwdUser.ToString(), Password = changePwdUser.ToString() });
			Assert.Fail();
		}
		catch (Exception ex)
		{
			Assert.IsInstanceOfType(ex, typeof(WrongUsernameOrPasswordError));
		}
	}
}
