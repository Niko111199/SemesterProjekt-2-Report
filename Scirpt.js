       function updateCharCount() {
            const content = document.getElementById("content").innerText;
            document.getElementById("charCounter").innerText = "Antal Tegn: " + content.length;
        }
        
        document.addEventListener("input", updateCharCount);
        updateCharCount();

        
        const pageNumbers = document.querySelectorAll('.SideNummer');
        const totalPages = pageNumbers.length;
        
        pageNumbers.forEach((div, index) => {
          div.textContent = `Side ${index + 1}/${totalPages}`;
        });