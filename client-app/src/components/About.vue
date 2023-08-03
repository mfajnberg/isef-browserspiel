<script setup>

</script>

<template>
    <div class='about'>
        <div class='about_content'>
            <h3 class="strong">Projekt</h3>
            <p>
                Diese Anwendung wurde im Rahmen des Kurses "Projekt Softwareengineering" an der IU Internationale Hochschule entwickelt.
            </p>
            <h3 class="strong">Team</h3>
            <div class="credits">
                <p>
                    <strong>
                        Maximilian Fajnberg –
                    </strong>
                    Projektleiter, Softwarearchitekt
                </p>
                <p>
                    <strong>
                        Stephan Ranger –
                    </strong>
                    Technische Leitung, Entwickler
                </p>
                <p>
                    <strong>
                        Michael Bordin –
                    </strong>
                    Anforderungsingeneur, technischer Redakteur
                </p>
            </div>
            <h3 class="strong">Creative Commons</h3>
            <div class="credits">
                <p>
                    <strong>
                        PetrDostal –
                    </strong>
                    "Medieval house" (3D Asset)
                </p>
                <p>
                    <strong>
                        chuckcg –
                    </strong>
                    "Medieval/Fantasy Props" (3D Asset)
                </p>
                <p>
                    <strong>
                        Sousinho –
                    </strong>
                    "Medieval Flag" (3D Asset)
                </p>
                <p>
                    <strong>
                        LucasPresoto –
                    </strong>
                    "Medieval Tent" (3D Asset)
                </p>
                <p>
                    <strong>
                        cattleya –
                    </strong>
                    "Low poly treasure chest" (3D Asset)
                </p>
                <p>
                    <strong>
                        AnnaBelle –
                    </strong>
                    "Fir Tree Stylized" (3D Asset)
                </p>
                <p>
                    <strong>
                        DJMaesen –
                    </strong>
                    "Ancient Tree" (3D Asset)
                </p>
                <p>
                    <strong>
                        Ivan Duch –
                    </strong>
                    "Fantasy Music - Magical Forest" (Musik)
                </p>
            </div>
            <br/>
            <div class="license">
                <p>
                    Alle sonstigen künstlerischen Darstellungen sind lizensiert unter CC BY-NC-ND 4.0 international.
                    <br/> 
                    Sie dürfen diese herunterladen und teilen, aber nicht bearbeiten oder für kommerzielle Zwecke verwenden.
                    <br/>
                    <a class="" href="https://creativecommons.org/licenses/by-nc-nd/4.0/legalcode.de" target="_blank">Klicken Sie hier, um eine Kopie dieser Lizenz einzusehen</a>
                    <br/>
                    <p class="strong">
                        ©2023 Maximilian Fajnberg
                    </p>
                </p>
            </div>
        </div>
    </div>
</template>

<style scoped>
    p {
        margin: 0%;
    }
    .about {
        display: flex;
        height: 100%;
        width: 60rem;
        background-color: rgba(0, 0, 0, 0.852);
        border-style: outset;
        border-width: 1px;
        border-color: rgba(133, 113, 86, 0.5) ;
        justify-content: center;
        overflow:auto;

        
    }
    .about_content{
        display: flex;
        flex-direction: column;
        font-size: .8rem;
        text-align: center;
    }
    
    .credits{
        width: 25rem;
        align-self: center;
        text-align: justify;
    }
    .license{
    }
</style>
