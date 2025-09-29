import { DayOfWeek } from "../../enums"
import { Car } from "./car-type"

export type Train = {
  id: number;
  name: string;
  dayOfWeek: DayOfWeek;
  cars: Car[];
}
