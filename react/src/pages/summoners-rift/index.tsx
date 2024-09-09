import InGameTools from "src/components/in-game-tools";

import styles from "./index.module.scss";

export default function Page() {
  return (
    <div>
      <InGameTools className={styles.tools} />
    </div>
  );
}
