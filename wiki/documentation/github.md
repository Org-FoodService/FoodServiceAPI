# ⬆️ GitHub

***

## Branches, Pull Requests, and Merges

In our development process, we utilize branches, pull requests, and merges to organize and integrate our code changes effectively. Here's our workflow:

### Creating a New Branch for a Card

When picking up a new card from our Kanban board [here](https://github.com/orgs/Org-FoodService/projects/3), always create a new branch based on the `develop` branch. The branch name should start with `issue_[issue_number_linked_to_the_card]`.

### Committing Changes

As you work on the task, make commits to your branch as needed. Each commit message should follow our Commit Message Conventions , with a prefix indicating the type of change and a concise description.

### Pull Request to Develop

After completing the task and making N commits, create a pull request to merge your branch into the `develop` branch. When creating the pull request, use the "Squash and merge" option to consolidate your commits into a single meaningful commit.

![image](https://github.com/Org-FoodService/DotNetMVCFoodService/assets/78824150/ed4882cd-2f50-4ee3-a70f-751799fa5586)

### Pull Request to Master

The pull request to merge changes into the `main`branch can only be made from the `develop` branch to `main`. When creating this pull request, choose the "Create a merge commit" option to maintain a clear history of changes.

![image](https://github.com/Org-FoodService/DotNetMVCFoodService/assets/78824150/981b8d42-b441-45cf-8cb6-82126408d7e5)

Following this workflow ensures that our codebase remains organized, changes are reviewed, and our development process is efficient and collaborative.

***

## Commit Message Conventions

When making commits to the repository, we follow a set of conventions to maintain consistency and clarity in our commit messages. Each commit message begins with a prefix indicating the type of change being made, followed by a concise description of the change.

### Prefix Definitions

| Prefix         | Definition                                                               |
| -------------- | ------------------------------------------------------------------------ |
| **FIX**🐛      | Used for bug fixes.                                                      |
| **PERF**🚀     | Indicates a performance improvement.                                     |
| **UX**🎨       | Denotes a user interface change.                                         |
| **SECURITY**🔒 | Signals a fix for a security problem.                                    |
| **FEAT**✨      | Represents an added feature.                                             |
| **DEV**🛠️     | Reserved for internals changes that don't fit into the above categories. |

### Example Commit Message

```plaintext
FEAT✨: #11 [Some comment for this commit]
```

In this example, the commit introduces a new feature related to issue #11, with a descriptive comment for this commit. When the commit is related to an issue, always link the issue number before the comment using #.

Adhering to these conventions helps us maintain a clear and organized commit history, making it easier to understand the changes introduced at a glance.
