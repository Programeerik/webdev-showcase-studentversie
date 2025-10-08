document.addEventListener("DOMContentLoaded", async function () {
    const joinForm = document.querySelector("#JoinForm");
    const createFrom = document.querySelector("#CreateForm");
    const btnStartGame = document.querySelector("#StartGame");

    const connection = await setupConnection();

    joinForm.addEventListener('submit', async function (event) {
        event.preventDefault();

        let groupName = joinForm.groupName.value;
        localStorage.setItem("GroupName", groupName);

        try {
            await connection.invoke("JoinGroup", groupName);
            removeForms();
        } catch (err) {
            console.error(err);
        }
    });

    btnStartGame.addEventListener('click', async function (event) {
        event.preventDefault();

        let groupName = localStorage.getItem("GroupName");

        try {
            connection.invoke("StartGame", groupName);
        } catch (err) {
            console.error(err);
        }
        
    })

    await connection.on("RemoveLobby", () => {
        removeUserList();
        removeStartGameBtn();
    });

    await connection.on("GameStarted", (groupName,playerSymbol) => {
        console.log(playerSymbol);

        createGameBoard(groupName,playerSymbol);

    });

    await connection.on("GroupFull", (groupName) => {
        alert(groupName + " heeft al het maximum van twee spelers.")
    })

    await connection.on("GroupMade", (groupName, userIds) => {
        var groupNameElement = document.querySelector("#GroupName");
        groupNameElement.innerHTML = groupName;
        createUserListElements(userIds);
        if (userIds.length >= 2) {
            btnStartGame.classList.remove("hide-game-element");
        }
    });

    await connection.on("JoinedGroup", (groupName, userIds) => {
        var groupNameElement = document.querySelector("#GroupName");
        groupNameElement.innerHTML = groupName;
        createUserListElements(userIds);
        if (userIds.length >= 2) {
            btnStartGame.classList.remove("hide-game-element");
        }
    });

    await connection.on("ShowUserList", (userIds) => {
        createUserListElements(userIds);
        if (userIds.length >= 2) {
            btnStartGame.classList.remove("hide-game-element");
        }
    });

    await connection.on("NotYourTurnMessage", (message) => {
        alert(message);
    });

    await connection.on("GameOver", (message) => {
        showGameEndPopUp(message);
    });

    createFrom.addEventListener('submit', async function (event) {
        event.preventDefault();

        const generatedGroupName = generateGroupName();
        localStorage.setItem("GroupName", generatedGroupName);

        try {
            await connection.invoke("CreateGroup", generatedGroupName);
            removeForms();
            
        } catch (err) {
            console.error(err);
        }
    });
    function generateGroupName() {

        let randomString = (Math.random() + 1).toString(36).substring(7);

        return randomString
    }

    function createUserListElements(userIds) {
        let ulEle = document.querySelector("#userList");

        if (ulEle.hasChildNodes) {
            ulEle.querySelectorAll("li").forEach((e) => ulEle.removeChild(e));
        }

        for (let i = 0; i < userIds.length; i++) {
            let liEle = document.createElement("li");
            liEle.innerHTML = userIds[i];
            ulEle.appendChild(liEle);
        }
    }

    function removeForms() {
        joinForm.remove();
        createFrom.remove();
    }

    function removeUserList() {
        let ulEle = document.querySelector("#userList");
        ulEle.remove();
    }

    function removeStartGameBtn() {
        btnStartGame.remove();
    }

    async function createGameBoard(groupName,playerSymbol) {
        const board = document.querySelector("#GameBoard");
        //board.setAttribute("style", "display:grid;width: 150px;grid-template-columns: auto auto auto;gap: 5px 5px;");
        for (let i = 0; i < 9; i++) {
            let span = document.createElement("div");
            span.innerHTML = " ";
            //span.style = "border:solid;#333333;1px;width:50px;height:50px;";
            span.id = "cell" + i;
            span.onclick = async () => {
                let cell = document.getElementById("cell" + i);
                if (cell.innerHTML == ' ') {
                    await connection.invoke("SendMove", groupName, playerSymbol, i);
                }
            };


            board.appendChild(span);
        }
    }

    connection.on("UpdateBoard", (playerSymbol, position) => {

        let cell = document.querySelector("#cell" + position);
        cell.innerHTML = playerSymbol;
    });

    connection.on("GameWon", (playerSymbol) => {
        for (let i = 0; i < 9; i++) {
            let span = document.getElementById("cell" + i);
            span.onclick = null;
        }

        const message = "<h1>" + playerSymbol + " heeft gewonnen!" + "</h1>";

        showGameEndPopUp(message);
    });

    function showGameEndPopUp(message) {
        const popup = document.createElement("div");
        popup.id = "GameEndPopUp";
        popup.innerHTML = message;

        const button = document.createElement("button");
        button.onclick = () => {
            connection.stop();
            hideGameEndPopUp();
            location.href = location.href;
        };
        button.innerHTML = "Terug naar beginscherm.";
        popup.appendChild(button);
        document.querySelector("body").appendChild(popup);
    }

    function hideGameEndPopUp() {
        document.querySelector("#GameEndPopUp").remove();
    }

    async function setupConnection() {
        const connection = new signalR.HubConnectionBuilder()
            .withUrl("http://localhost:5001/hub/game")
            .configureLogging(signalR.LogLevel.Information)
            .build();

        async function start() {
            try {
                await connection.start();
                console.log("SignalR Connected.");
            } catch (err) {
                console.log(err);
                setTimeout(start, 5000);
            }
        };

        connection.onclose(async () => {
            await start();
        });

        await start();
        return connection;
    }
});


