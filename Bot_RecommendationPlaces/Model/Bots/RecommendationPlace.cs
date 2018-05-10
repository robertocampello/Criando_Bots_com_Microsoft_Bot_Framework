using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;

namespace Bot_RecommendationPlaces.Model.Bots
{
    // A classe RecommendationPlace representa o form que será utilizado para definir a comversação com o usuário.
    // Deve ser serializable, então o bot pode ser stateless.
    // A ordem dos fields define a sequencia default na qual o usuário questionado.

    // Os enumns definem as opções para cada field do form RecommendationPlace e a ordem dos valores representa a sequência
    // na qual eles serão apresentados para o usuário na conversação.
    [Serializable]
    public class RecommendationPlace {
        /// <summary>
        /// Define os locais
        /// </summary>
        public enum PlaceOptions {
            [Describe("Rio de Janeiro")]
            Rio_de_Janeiro,
            [Describe("São Paulo")]
            SaoPaulo,
            [Describe("Miami")]
            Miami,
            [Describe("Orlando")]
            Orlando,
            [Describe("New York")]
            NewYork,
            [Describe("Paris")]
            Paris,
            [Describe("Londres")]
            Londres
        }

        /// <summary>
        /// Define os tipos de lugares que o usuário deseja recomendação
        /// </summary>
        public enum TypePlaces {
            Shoppings,
            Supermercados,
            Parques,
            Restaurantes,
            Cinemas
        }
        
        // & fieldname, || tipos desejados
        [Prompt("Em local você se encontra? {||} Digite a sua opção abaixo.")]
        public PlaceOptions? Place { get; set; }

        // & fieldname, || tipos desejados
        [Prompt("Que tipo de local você deseja ir? {||} Digite a sua opção abaixo.")]
        public TypePlaces? Type { get; set; }

        /// <summary>
        /// Método que permite que o nosso bot converse com nossos usuários.
        /// </summary>
        /// <returns></returns>
        public static IForm<RecommendationPlace> BuildForm() {
            return new FormBuilder<RecommendationPlace>()
                .Message("Seja bem vindo ao Recommendation Place! O local onde você encontra as melhores recomendações.")
                .AddRemainingFields()
                .OnCompletion(async (context, profileForm) =>
                {
                    string       message = null;
                    List<String> places  = new List<String>();

                    // Define recomendação de acordo com as escolhas do usuário
                    switch (profileForm.Place) {
                        case PlaceOptions.Orlando :
                            switch (profileForm.Type) {
                                case TypePlaces.Parques :
                                    // Adicona recomendações na lista
                                    places.Add("Disney's Magic Kingdom");
                                    places.Add("Disney's Hollywood Studios");
                                    places.Add("Epcot Center");
                                    places.Add("SeaWorld");
                                    places.Add("Universal Studios Florida");
                                    places.Add("Universal's Islands of Adventure");
                                    places.Add("Universal's Volcano Bay");
                                    places.Add("Lego Land");
                                    break;
                            }
                            break;
                    }

                    // Valida lista de recomendações
                    if (places.Count > 0) {
                        StringBuilder stringPlaces = new StringBuilder();
                        places.ForEach(delegate (String place) {
                            stringPlaces.Append("<br/>").Append(place); 
                        });

                        message = String.Format("Segue a lista de recomendações: {0}", stringPlaces);
                    } else {
                        message = "Não temos recomendação de lugares no momento para a consulta solicitada.";
                    } // end else

                    // Retorna message
                    await context.PostAsync(message);
                })
                .Build();
        }
    }
}