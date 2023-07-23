### Project 3 Fusion - Spark

**This project is an experimental project to see the limits of blockchain gaming.** As a team of three people, we open sourcely develop a web3 game development kit **(There is only example game right now, we currently focus on creating a stable example game. After that we will convert this game to a game development kit)** that enables web3 game developers to build more complex web3 games. The real methodolgy behind is creating a validator, player and smart contract trio whose connectivity level can be adjusted before deploying the contract related to the speed of the network, complexity of the game etc. This means, validator can be in full, half or little contact with smart contract. Also keep in mind that what we call validator, is a game server with specific web3 software embeded to it.

**There is an important thing to note,** it is impossible to place a complex game to the blockchain right now due to the speeds. This project includes an example moba game as starting point which takes 7000 inputs per player in each game which leads to 70000 inputs in total game. Here is the great question: Can a blockchain network handle 70000 clicks just for a single game? Maybe yes, maybe not. We'll see at the future. That's why, keep in mind that if you increase your complexity way more higher, the more network needs to transfer the data.

That's why, we use a random validator which is assigned to the network and giving its credentials to the smart contract. Later, smart contract informs players to connect to the validator to play the game.

What we want to imply that, with using a validator we are able to give heavy work to the validator while smart contract is handling the data storage and basic calculations. This validator can be full, half or small chain connected validator. Here is a quick diagram for everyone to understand the simple architecture of project 3 fusion (Will be added later.)

What we will try to do is,
- Data transfer between player, smart contract and validator
- Connectivity adjustment between player, smart contract and validator
- Smart calculations inside both smart contract and validator
- More can be added here...

**We would like to note again that, we are focused on creation of a working web3 moba game with the architecture we described. After this architecture is implemented, we will convert example project to a web3 game development kit.**

### Goals

We aim to create tools that are useful for anyone working on the web3 games. Although it is not certain, creation of web3 moba game can be continued with extra templates and more tools.

### Tools

Here is the tools that are being developed in this project,

- Argent Wallet Connector
- Game Validator
- Cairo 2 Game Contract Examples
- Game Example

### Versions

- v1: Small Chain Integration

### Used Technologies

- Smart Contract: Cairo 2
- Game: Unity Engine
- Validator: Unity Netcode
