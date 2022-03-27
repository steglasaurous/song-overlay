import {SupportedComponentsModel} from "../model/supported-components.model";
import {Observable} from "rxjs";

export interface GameDataServiceInterface {
  /**
   * Return the name of the game or service.  Used when needing to filter based on a specific game/service.
   */
  getName(): string;

  /**
   * Tells the overlay what this particular service supports on the overlay.
   */
  supports(): SupportedComponentsModel;

  connect(options: any): Observable<any>;
}
